using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> AddMovie(Movie movie)
        {
            var newMovie = await _adminService.AddMovie(movie);
            return Ok(newMovie);
        }
        [HttpPut]
        [Route("movie")]
        public async Task<IActionResult> UpdateMovie(Movie movie)
        {
            var updatedMovie = await _adminService.UpdateMovie(movie);
            return Ok(updatedMovie);
        }

        [HttpGet]
        [Route("top-purchased-movies")]
        public async Task<IActionResult> GetTopPurchases([FromQuery] DateTime? fromDate = null, 
            [FromQuery] DateTime? toDate = null, [FromQuery] int pageSize = 30, [FromQuery] int pageIndex = 1)
        {
            var movies = await _adminService.GetTopPurchasedMovies(fromDate, toDate, pageSize, pageIndex);
            if (movies == null || !movies.Any())
            {
                // 404
                return NotFound(new { errorMessage = "No Movies Found" });
            }
            return Ok(movies);
        }
    }
}
