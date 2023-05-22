using Admin.Data;
using Admin.Models.Identity;
using Admin.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _user;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(IUserService user, UserManager<ApplicationUser> userManager)
        {
            _user = user;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
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
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }

            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> Logout()
        {
            await _user.Logout();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login");
        }
    }
}
