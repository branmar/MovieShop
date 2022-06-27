using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        // all these action methods should only be execute when user is loged in

        private readonly ICurrentLoggedInUser _currentLogedInUser;
        public UserController(ICurrentLoggedInUser currentLogedInUser)
        {
            _currentLogedInUser = currentLogedInUser;
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            // go to database and get all the movies purchased by user, user id
            // var cookie = this.HttpContext.Request.Cookies["MovieShopAuthCookie"];
            // can create a class the exposes HttpContext cooke decrypted info and claims
            var userId = _currentLogedInUser.UserId;
            // use the UserId and send to User Serivce to get information for that User 
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuyMovie()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            return View();
        }
    }
}
