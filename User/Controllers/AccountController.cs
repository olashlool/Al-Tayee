using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Admin.Models.Identity;
using Admin.Models.ViewModels;
using Admin.Data;
using System.Text.RegularExpressions;

namespace User.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        string culture = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
        public AccountController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
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
                await _userService.Register(viewModel);
                return View("Login");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
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
                var result = await _userService.Login(viewModel);
                if (result == 1)
                {
                    var user = await _userManager.FindByEmailAsync(viewModel.Email);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    var errorMessage = culture.StartsWith("en") ? "Wrong email or password!" : "البريد الإلكتروني أو كلمة المرور خاطئة!";
                    ModelState.AddModelError("", errorMessage);
                    return View();
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
            await _userService.Logout();
            return RedirectToAction(nameof(Login));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ProfileAccount()
        {
            var loggedInUser = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(loggedInUser);
            var userInfo = new UserInfo
            {
                Id = user.Id.ToString(),
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                Day = user.Day,
                Month = user.Month,
                Year = user.Year
            };
            return View(userInfo);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateProfileAccount(string id, UserInfo updateUser)
        {
            if (!ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                await _userService.Update(user, updateUser);
            }
            return RedirectToAction(nameof(ProfileAccount));
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

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var passwordCorrect = await _userManager.CheckPasswordAsync(user, model.OldPassword);

            if (!passwordCorrect)
            {
                var errorMessage = culture.StartsWith("en") ? "Old password doesn't match" : "كلمة المرور القديمة غير صحيحة";
                ModelState.AddModelError(string.Empty, errorMessage);
                return View(model);
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            var passChanged = culture.StartsWith("en") ? "Password was changed." : "تم تغيير كلمة المرر.";
            TempData["BarNotificationDiv"] = "<div id=\"bar-notification\" class=\"bar-notification-container\" data-close=\"Close\">" +
                "<div class=\"bar-notification success\" style=\"display: block;\">" +
                "<p class=\"content\">" + passChanged + "</p>" +
                "<span class=\"close\" title=\"Close\"></span>" +
                "</div></div>";

            TempData["Message"] = "Password changed successfully";
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return RedirectToAction(nameof(Login));
        }
    }
}
