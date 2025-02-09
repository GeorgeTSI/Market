using MarketAPI.Data;
using Microsoft.EntityFrameworkCore;
using MarketAPI.Entities;

namespace MarketAPI.Repositories
{
    public class MarketRepository(AppDbContext _context) : IMarketRepository
    {
        public async Task<List<Market>> GetAllMarketsAsync()
        {
            return await _context.Markets.ToListAsync();
        }
    }
}
