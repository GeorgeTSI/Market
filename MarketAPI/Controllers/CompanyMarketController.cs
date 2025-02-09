using Microsoft.AspNetCore.Mvc;
using MarketAPI.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace MarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyMarketController(ICompanyRepository _companyRepo, IMarketRepository _marketRepo) : ControllerBase
    {
        [HttpGet("companies")]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyRepo.GetAllCompaniesAsync();
            return Ok(companies);
        }

        [HttpGet("markets")]
        public async Task<IActionResult> GetMarkets()
        {
            var markets = await _marketRepo.GetAllMarketsAsync();
            return Ok(markets);
        }
    }
}

