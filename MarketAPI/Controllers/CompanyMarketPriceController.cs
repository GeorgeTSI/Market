using MarketAPI.Entities;
using MarketAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyMarketPriceController(ICompanyMarketPriceRepository _repository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPrices()
        {
            var prices = await _repository.GetAllPricesAsync();
            return Ok(prices);
        }

        [HttpGet("{companyId}/{marketId}")]
        public async Task<IActionResult> GetPrice(int companyId, int marketId)
        {
            var price = await _repository.GetPriceByCompanyAndMarketAsync(companyId, marketId);
            if (price == null)
            {
                return NotFound();
            }
            return Ok(price.Price);
        }

        [HttpPut("{companyId}/{marketId}")]
        public async Task<IActionResult> UpdatePrice(int companyId, int marketId, [FromBody] PriceUpdateDto priceUpdate)
        {
            var updatedPrice = await _repository.UpdatePriceAsync(companyId, marketId, priceUpdate.NewPrice);
            if (updatedPrice == null)
            {
                return NotFound();
            }
            return Ok(updatedPrice);
        }
    }
}