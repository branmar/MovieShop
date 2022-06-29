using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {

        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("top-grossing")]
        // Attribute Routing
        // MVC http://localhost/movies/GetTopGrossingMovies => Traditional/Convention based routing
        // http://localhost/api/movies/top-grossing
        public async Task<IActionResult> GetTopGrossingMovies()
        {
            // call my service
            var movies = await _movieService.GetTopGrossingMovies();
            // return the movies information in JSON Format
            // ASP.NET Core automatically serializes C# objects to JSOn objects
            // System.Text.Json .NET 3
            // older version of .NET to work with JSON Newtonsoft.JSON
            // return data(json format) along with it return the HTTP status code

            if (movies == null || !movies.Any())
            {
                // 404
                return NotFound(new { errorMessage = "No Movies Found" });
            }

            return Ok(movies);

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                return NotFound(new { errorMessage = $"No Movie Found for id: {id}" });
            }

            return Ok(movie);
        }
        [HttpGet]
        [Route("")]
        // Need to implement
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieService.GetTopGrossingMovies();
            return Ok(movies);

        }

        [HttpGet]
        [Route("top-rated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTopRatedMovies();

            return Ok(movies);
        }
        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieService.GetMoviesByGenre(genreId);
            if (movies == null || movies.TotalRecords == 0)
            {
                return NotFound(new { errorMessage = $"No Movies Found for Genre Id: {genreId}" });
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetMovieReviews(int id, int pageSize = 30, int pageNumber = 1)
        {
            var reviews = await _movieService.GetReviewsByMovie(id, pageSize, pageNumber);
            if (reviews == null || reviews.TotalRecords == 0)
            {
                return NotFound(new { errorMessage = $"No Reviews Found For Movie Id: {id}" });
            }
            return Ok(reviews);
        }
    }
}
