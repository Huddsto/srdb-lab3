using Microsoft.AspNetCore.Mvc;
using OnlineGamesStore.Dtos;
using OnlineGamesStore.Implementations.Services.Interfaces;

namespace OnlineGamesStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _service;

        public PurchaseController(IPurchaseService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddSale(AddSaleDto dto)
        {
            try
            {
                await _service.AddSaleAsync(dto);
                return Ok(new { message = "Purchase added successfully!" });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }

}
