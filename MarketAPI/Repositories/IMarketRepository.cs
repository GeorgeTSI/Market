using MarketAPI.Entities;

namespace MarketAPI.Repositories
{
    public interface IMarketRepository
    {
        Task<List<Market>> GetAllMarketsAsync();
    }
}
