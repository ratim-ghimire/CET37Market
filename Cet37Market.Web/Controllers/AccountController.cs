namespace Cet37Market.Web.Controllers
{
    using Cet37Market.Web.Data.Entities;
    using Cet37Market.Web.Helpers;
    using Cet37Market.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;

    public class AccountController : Controller
    {
        private readonly IUserHelper userHelper;

        public AccountController(IUserHelper userHelper)
        {
            this.userHelper = userHelper;
        }
        //Method for login
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index","Home");
            }
            return this.View();
        }
        //Login ActionResult Post
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    //to return to same page where login is requested
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))  //saves the URLPath
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    //IF not goto Home
                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to login");
            return this.View(model);
        }

        //Method for logout
        public async Task<IActionResult> Logout()
        {
            await this.userHelper.LogoutAsync();
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            //Create the user if all the model state is valid
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmail(model.Username); //Sees if the user already exists
                if(user == null) //If not create the new user
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email  =model.Username,
                        UserName  = model.Username
                    };

                    var result = await this.userHelper.AddUserAsync(user, model.Password);
                    
                    if(result != IdentityResult.Success) // If Register is not success, adds model error to view
                    {
                        this.ModelState.AddModelError(string.Empty, "Error, The user couldnt be registrered....");
                        return this.View(model);
                    }
                    //If the register is success , login the user
                    var loginViewModel = new LoginViewModel
                    {
                        Password = model.Password,
                        UserName = model.Username,
                        RememberMe = false
                    };
                    //If login is success redirect to home
                    var result2 = await this.userHelper.LoginAsync(loginViewModel);
                    if (result2.Succeeded)
                    {
                        return this.RedirectToAction("Index", "Home");
                    }
                    this.ModelState.AddModelError(string.Empty, "Error, The user couldnt be logged in....");
                    return this.View(model);
                }
                this.ModelState.AddModelError(string.Empty, "Error, UserName already exists..");
            }
            return this.View(model);
        }
    }
}