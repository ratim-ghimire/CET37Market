namespace Cet37Market.Web.Helpers
{
    using System;
    using System.Threading.Tasks;
    using Cet37Market.Web.Data.Entities;
    using Microsoft.AspNetCore.Identity;

    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> userManager;

        public UserHelper(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IdentityResult> AddUserAsync(User user, string Password)
        {
            return await this.userManager.CreateAsync(user, Password);
        }

        public async Task<User> GetUserByEmail(string Email)
        {
            return await this.userManager.FindByEmailAsync(Email);
        }
    }
}
