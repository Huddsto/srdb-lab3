using Microsoft.AspNetCore.Mvc;
using OnlineGamesStore.Implementations.Services.Interfaces;
using OnlineGamesStore.Models;
using OnlineGamesStore.Persistence.DbContext;

namespace OnlineGamesStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameDetails(int id)
        {
            var result = await _gameService.GetGameDetailsAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
