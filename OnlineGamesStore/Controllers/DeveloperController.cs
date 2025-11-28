using Microsoft.AspNetCore.Mvc;
using OnlineGamesStore.Implementations.Services.Interfaces;

namespace OnlineGamesStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperService _developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeveloperById(int id)
        {
            var result = await _developerService.GetDeveloperByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDevelopers()
        {
            var result = await _developerService.GetAllDevelopersAsync();
            return Ok(result);
        }
    }
}
