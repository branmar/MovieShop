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
        public IActionResult Details(int id)
        {
            var movie = _movieService.GetMovieDetails(id);
            return View(movie);
        }
    }
}
