namespace Cet37Market.Web.Controllers
{
    using Cet37Market.Web.Data.Entities;
    using Cet37Market.Web.Helpers;
    using Cet37Market.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using System;

    public class AccountController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IConfiguration configuration;

        public AccountController(IUserHelper userHelper, IConfiguration configuration)
        {
            this.userHelper = userHelper;
            //Iconfig helps to get to app setting to inject in controller(Tokens)
            this.configuration = configuration;
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

        public async Task<IActionResult> ChangeUser()
        {
            var user = await this.userHelper.GetUserByEmail(this.User.Identity.Name);

            var model = new ChangeUserViewModel();
            
            if(user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
            }
            return this.View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmail(this.User.Identity.Name);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    var response = await this.userHelper.UpdateUserAsync(user);
                    if (response.Succeeded)
                    {
                        this.ViewBag.UserMessage = "User updated";
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            return this.View(model);
        }

        public IActionResult ChangePassword()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmail(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await this.userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmail(model.UserName);
                if (user != null)
                {
                    var result = await this.userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Tokens:Key"]));
                        var credencials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            this.configuration["Tokens:Issuer"],
                            this.configuration["Tokens:Audience"],
                            claims,
                            //Use UTC datetime in web
                            expires: DateTime.UtcNow.AddDays(15),
                            signingCredentials: credencials);

                        //Dynamic property(anonymous type)
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return this.Created(string.Empty, results);
                    }
                }
            }

            return this.BadRequest();
        }

    }
}