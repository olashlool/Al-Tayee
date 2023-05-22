using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using Admin.Models.Identity;
using Admin.Data;
using Admin.Models.ViewModels;

namespace User.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _user;
        private readonly UserManager<ApplicationUser> _userManager;
        string culture = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;


        public AccountController(IUserService user, UserManager<ApplicationUser> userManager)
        {
            _user = user;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM viewModel)
        {
            viewModel.UserName = viewModel.FirstName + viewModel.LastName;
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await _user.Register(viewModel);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
            return View("Login");
        }
        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Account/Login")]
        public async Task<IActionResult> Login(LoginVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var result = await _user.Login(viewModel);
                if (result == 1)
                {
                    var user = await _userManager.FindByEmailAsync(viewModel.Email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (culture.StartsWith("en"))
                    {
                        throw new Exception("Wrong email or password!");
                    }
                    else
                    {
                        throw new Exception("البريد الإلكتروني أو كلمة المرور خاطئة!");
                    }
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }

        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _user.Logout();
            return RedirectToAction("Login");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ProfileAccount()
        {
            UserInfo userInfo = new UserInfo();
            var loggedInUser = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(loggedInUser);
            userInfo = new UserInfo
            {
                Id = user.Id.ToString(),
                UserName= user.UserName,                                                                                            
                Email=user.Email,
                FirstName=user.FirstName,
                SecondName=user.SecondName,
                BirthDate=user.BirthDate,
                Gender=user.Gender,
                PhoneNumber = user.PhoneNumber
            };
            return View(userInfo);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateProfileAccount(string id, UserInfo updateUser)
        {
            var user = await _userManager.FindByIdAsync(id);
            //var loggedInUser = User.Identity.Name;
            //var user = await _userManager.FindByNameAsync(you);

            await _user.Update(user, updateUser);

            return RedirectToAction("ProfileAccount");
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Retrieve the user by their ID
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                // Handle the case where the user doesn't exist
                return NotFound();
            }

            // Validate the user's current password
            var passwordCorrect = await _userManager.CheckPasswordAsync(user, model.OldPassword);

            if (!passwordCorrect)
            {
                // Handle the case where the user's current password is incorrect
                if (culture.StartsWith("en"))
                {
                    ModelState.AddModelError(string.Empty, "Old password doesn't match");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "كلمة المرور القديمة غير صحيحة");
                }
                return View(model);
            }

            // Update the user's password
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                // Handle the case where the password change fails
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            //// Sign the user back in with their new password
            //await _signInManager.RefreshSignInAsync(user);

            // Redirect to a success page or return a success message
            TempData["Message"] = "Password changed successfully";
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login");
        }

    }
}
