using MarketAPI.Data;
using MarketAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Repositories
{
    public class CompanyMarketPriceRepository(AppDbContext context) : ICompanyMarketPriceRepository
    {
        private readonly AppDbContext _context = context;
        public async Task<IEnumerable<CompanyMarketPrice>> GetAllPricesAsync()
        {
            return await _context.CompanyMarketPrices
             .Include(cmp => cmp.Company)  
             .Include(cmp => cmp.Market)  
             .ToListAsync();
        }

        public async Task<CompanyMarketPrice> GetPriceByCompanyAndMarketAsync(int companyId, int marketId)
        {
            var price = await _context.CompanyMarketPrices
            .FirstOrDefaultAsync(cmp => cmp.CompanyId == companyId && cmp.MarketId == marketId);

            return price ?? throw new KeyNotFoundException($"No price found for company {companyId} in market {marketId}");
        }

        public async Task<CompanyMarketPrice> UpdatePriceAsync(int companyId, int marketId, decimal newPrice)
        {
            var price = await _context.CompanyMarketPrices
             .FirstOrDefaultAsync(cmp => cmp.CompanyId == companyId && cmp.MarketId == marketId);

            if (price != null)
            {
                price.Price = newPrice;
                await _context.SaveChangesAsync();
            }
            
            return price ?? throw new KeyNotFoundException($"No price updated for company {companyId} in market {marketId}"); ;
        }
    }
}