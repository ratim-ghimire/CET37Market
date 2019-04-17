

namespace Cet37Market.Web.Helpers
{
    using System.Threading.Tasks;
    using Cet37Market.Web.Data.Entities;
    using Microsoft.AspNetCore.Identity;

    public interface IUserHelper
    {
        Task<User> GetUserByEmail(string Email);
        Task<IdentityResult> AddUserAsync(User user, string Password);  

    }
}
