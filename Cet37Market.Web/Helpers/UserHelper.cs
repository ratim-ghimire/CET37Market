namespace Cet37Market.Web.Helpers
{
    using System;
    using System.Threading.Tasks;
    using Cet37Market.Web.Data.Entities;
    using Cet37Market.Web.Models;
    using Microsoft.AspNetCore.Identity;

    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserHelper(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IdentityResult> AddUserAsync(User user, string Password)
        {
            return await this.userManager.CreateAsync(user, Password);
        }
        //Identity result for change password
        public async Task<IdentityResult> ChangePasswordAsync(User user, string OldPassword, string NewPassword)
        {
            return await this.userManager.ChangePasswordAsync(user, OldPassword, NewPassword);
        }

        public async Task<User> GetUserByEmail(string Email)
        {
            return await this.userManager.FindByEmailAsync(Email);
        }

        //Method for login
        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await this.signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
                false
               );
        }

        //Methos for logout
        public async Task LogoutAsync()
        {
            await this.signInManager.SignOutAsync();
        }
        //Update User
        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await this.userManager.UpdateAsync(user);
        }

        public async Task<SignInResult> ValidatePasswordAsync(User user, string password)
        {
            return await this.signInManager.CheckPasswordSignInAsync(user, password, false);
        }
    }
}
