using MarketAPI.Entities;

namespace MarketAPI.Repositories
{
    public interface ICompanyMarketPriceRepository
    {
        Task<IEnumerable<CompanyMarketPrice>> GetAllPricesAsync();
        Task<CompanyMarketPrice> GetPriceByCompanyAndMarketAsync(int companyId, int marketId);
        Task<CompanyMarketPrice> UpdatePriceAsync(int companyId, int marketId, decimal newPrice);
    }
}

