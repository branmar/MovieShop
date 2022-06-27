using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // showing details of the movie
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            return View(movie);
        }

        public async Task<IActionResult> Genre(int id, int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovies = await _movieService.GetMoviesByGenre(id, pageSize, pageNumber);
            return View("PagedMovies", pagedMovies);
        }
    }
}
