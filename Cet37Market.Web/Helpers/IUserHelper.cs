namespace Cet37Market.Web.Helpers
{
    using System.Threading.Tasks;
    using Data.Entities;
    using Models;
    using Microsoft.AspNetCore.Identity;

    public interface IUserHelper
    {
        Task<User> GetUserByEmail(string Email);
        Task<IdentityResult> AddUserAsync(User user, string Password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();
    }
}
