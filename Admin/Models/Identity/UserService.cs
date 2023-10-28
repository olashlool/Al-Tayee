using Admin.Data;
using Admin.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Admin.Models.Identity
{
    public class UserService : IUserService
    {
        string culture = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<int> Login(LoginVM viewModel)
        {
            var result = new SignInResult();
            var user = await _userManager.FindByEmailAsync(viewModel.Email);
            if (user != null)
            {
                result = await _signInManager.PasswordSignInAsync(user.UserName, viewModel.Password, true, false);
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
            if (!result.Succeeded)
            {
                return 0;
            }
            return 1;
        }

        public async Task Register(RegisterVM model)
        {
            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                if (culture.StartsWith("en"))
                {
                    throw new Exception("Something went wrong! Username is used before");
                }
                else
                {
                    throw new Exception("هناك خطأ ما! اسم المستخدم مستخدم من قبل");
                }
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                if (culture.StartsWith("en"))
                {
                    throw new Exception("Something went wrong! Email is used before.");
                }
                else
                {
                    throw new Exception("هناك خطأ ما! الإيميل مستخدم من قبل");
                }
            }

            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.PhoneNumber, FirstName = model.FirstName, SecondName = model.LastName, Gender = model.Gender != null ? model.Gender : "", Day = model.Day, Month= model.Month, Year = model.Year};
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                if (culture.StartsWith("en"))
                {
                    throw new Exception("Something went wrong! Make sure the password has small letters, capital letters, numbers, and symbols!");
                }
                else
                {
                    throw new Exception("هناك خطأ ما! تأكد من أن كلمة المرور تحتوي على أحرف صغيرة وأحرف كبيرة وأرقام ورموز!");
                }
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task Update(ApplicationUser user, UserInfo updateUser)
        {
            //user.UserName = updateUser.UserName;
            user.FirstName = updateUser.FirstName;
            user.SecondName = updateUser.SecondName;
            user.Gender = updateUser.Gender;
            user.PhoneNumber = updateUser.PhoneNumber;
            user.Email = updateUser.Email;
            user.BirthDate = updateUser.BirthDate;
            user.Day= updateUser.Day;
            user.Month = updateUser.Month;
            user.Year = updateUser.Year;

            var result = await _userManager.UpdateAsync(user);

        }
    }
}
