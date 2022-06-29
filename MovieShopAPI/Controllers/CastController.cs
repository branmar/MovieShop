using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Contracts.Services;


namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : Controller
    {
        private readonly ICastService _castService;
        public CastController(ICastService castService)
        {
            _castService = castService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCastMemberDetails(int id)
        {
            var castMember = await _castService.GetCastDetails(id);
            if (castMember == null)
            {
                return NotFound(new { errorMessage = $"No Cast Member Found for id: {id}" });
            }
            return Ok(castMember);
        }
    }
}
