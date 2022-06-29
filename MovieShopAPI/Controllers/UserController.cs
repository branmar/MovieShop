using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) 
        { 
            _userService = userService; 
        }

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetMoviesPurchasedByUser(int id)
        {
            // we need to get the userId from the token, using HttpContext
            var movies = await _userService.GetAllPurchasesForUser(id);
            if (movies == null)
            {
                return NotFound(new { errorMessage = $"No Movies Found for id: {id}" });
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> GetUserDetails(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { errorMessage = $"No User Found for id: {id}" });
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("purchase-movie")]
        public async Task<IActionResult> PurchaseMovie(PurchaseRequestModel purchaseRequest, int id)
        {
            var purchase = await _userService.PurchaseMovie(purchaseRequest, id);
            if (purchase == false)
            {
                return NotFound(new { errorMessage = $"Movie not found" });
            }
            return Ok(purchase);
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = await _userService.AddFavorite(favoriteRequest);
            return Ok(favorite);
        }

        [HttpPost]
        [Route("un-favorite")]
        public async Task<IActionResult> RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var favorite = await _userService.RemoveFavorite(favoriteRequest);
            if (favorite == false)
            {
                return NotFound(new { errorMessage = $"Favorite not found" });
            }
            return Ok(favorite);
        }
        [HttpGet]
        [Route("check-movie-favorite/{movieId:int}")]
        public async Task<IActionResult> CheckIfMovieFavorite(int userId, int movieId)
        {
            var movie = await _userService.FavoriteExists(userId, movieId);
            return Ok(movie);
        }

        [HttpPost]
        [Route("add-review")]
        public async Task<IActionResult> AddReview(ReviewRequestModel reviewRequest)
        {
            var review = await _userService.AddMovieReview(reviewRequest);
            return Ok(review);
        }

        [HttpPut]
        [Route("edit-review")]
        public async Task<IActionResult> EditReview(ReviewRequestModel reviewRequest)
        {
            var review = await _userService.UpdateMovieReview(reviewRequest);
            return Ok(review);
        }

        [HttpDelete]
        [Route("delete-review/{movieId:int}")]
        public async Task<IActionResult> DeleteReview(int userId, int movieId)
        {
            var review = await _userService.DeleteMovieReview(userId, movieId);
            if (review == false)
            {
                return NotFound(new { errorMessage = $"Review not found for User {userId} on Movie {movieId}" });
            }
            return Ok(review);
        }

        [HttpGet]
        [Route("purchase-details/{movieId:int}")]
        public async Task<IActionResult> GetPurchaseDetails(int userId, int movieId)
        {
            var purchaseDetails = await _userService.GetPurchasesDetails(userId, movieId);
            if (purchaseDetails == null)
            {
                return NotFound(new { errorMessage = $"No Purchase Found for movie id: {movieId} by user: {userId}" });
            }
            return Ok(purchaseDetails);
        }

        [HttpGet]
        [Route("check-movie-purchased/{movieId:int}")]
        public async Task<IActionResult> CheckIfMoviePurchased(PurchaseRequestModel purchaseRequest, int id)
        {
            var purchase = await _userService.IsMoviePurchased(purchaseRequest, id);
            return Ok(purchase);
        }

        [HttpGet]
        [Route("favorites")]
        public async Task<IActionResult> GetAllFavorites(int id)
        {
            var favorites = await _userService.GetAllFavoritesForUser(id);
            return Ok(favorites);
        }

        [HttpGet]
        [Route("movie-reviews")]
        public async Task<IActionResult> GetAllReviews(int id)
        {
            var reviews = await _userService.GetAllReviewsByUser(id);
            return Ok(reviews);
        }
    }
}
