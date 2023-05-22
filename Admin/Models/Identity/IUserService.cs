using Admin.Data;
using Admin.Models.ViewModels;

namespace Admin.Models.Identity
{
    public interface IUserService
    {
        Task Register(RegisterVM viewModel);
        Task<int> Login(LoginVM viewModel);
        Task Logout();
        Task Update(ApplicationUser user, UserInfo updateUser);
    }
}
