using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var user = await _accountService.ValidateUser(model.Email, model.Password);
            if (user != null)
            {
                // redirect to home page
                // create a cookie, cookies are always sent from browser automatically to server
                // Inside the cookie we store encrypted information (User claims) that Server can recognize and tell whether user is logged in or not
                // Cookie should have an expration time
                // 2 hours
                var claims = new List<Claim>
                {
                    new Claim( ClaimTypes.Email, model.Email ),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToShortDateString()),
                    new Claim(ClaimTypes.Country, "USA"),
                    new Claim("Language", "English"),
                };

                // create cookie and cookie will have the above claims information along with expiration time
                // dont store above information in the cookie as plain text, instead encrypt the above information

                // we need to tell ASP.NET application that we are using Cookie based Authentication so that ASP.NET can generate Cookie based on the settings we provide

                // Identity object that will identify the user with claims
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Principal object which is used by ASP.NET to recognize the USER
                // Create the Cookie whith above information

                // HttpContext => Encapsualtes your Http Request information

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return LocalRedirect("~/");


            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            // show the empty register Page when we make a GET Request
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            // Model Binding 
            var user = await _accountService.RegisterUser(model);
            // redirect to logn page
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
