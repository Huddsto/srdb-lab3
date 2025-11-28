using Microsoft.AspNetCore.Mvc;
using OnlineGamesStore.Implementations.Services.Interfaces;
using OnlineGamesStore.Implementations.Services;

namespace OnlineGamesStore.Controllers
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _service;

        public StatisticsController(IStatisticsService service)
        {
            _service = service;
        }

        [HttpGet("games/less-than-avg")]
        public async Task<IActionResult> GetGamesLessThanAvg()
        {
            return Ok(await _service.GetGamesLessThanAvgAsync());
        }

        [HttpGet("games/count-by-price/{price}")]
        public async Task<IActionResult> GetGamesCountByPrice(decimal price)
        {
            return Ok(await _service.GetGamesCountByPriceAsync(price));
        }

        [HttpGet("orders/max/{gameName}")]
        public async Task<IActionResult> GetMaxOrderByGame(string gameName)
        {
            return Ok(await _service.GetMaxOrderByGameNameAsync(gameName));
        }

        [HttpGet("orders/expensive/{date}")]
        public async Task<IActionResult> GetMostExpensive(string date)
        {
            DateTime parsed = DateTime.Parse(date);
            return Ok(await _service.GetMostExpensiveGameByDateAsync(parsed));
        }
    }
}
