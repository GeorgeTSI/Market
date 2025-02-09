using MarketAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<CompanyMarketPrice> CompanyMarketPrices { get; set; }

        public static void Seed(AppDbContext context)
        {
            if (!context.Companies.Any())
            {
                var companies = new List<Company>
                {
                    new() { Id = 1, Name = "Company A", CompanyMarketPrices = [] },
                    new() { Id = 2, Name = "Company B", CompanyMarketPrices = [] },
                    new() { Id = 3, Name = "Company C", CompanyMarketPrices = [] },
                    new() { Id = 4, Name = "Company D", CompanyMarketPrices = [] },
                    new() { Id = 5, Name = "Company E", CompanyMarketPrices = [] }
                };

                context.Companies.AddRange(companies);
                context.SaveChanges();
            }

            if (!context.Markets.Any())
            {
                var markets = new List<Market>
                {
                    new() { Id = 1, Name = "Market X", CompanyMarketPrices = [] },
                    new() { Id = 2, Name = "Market Y", CompanyMarketPrices = [] },
                    new() { Id = 3, Name = "Market Z", CompanyMarketPrices = [] }
                };

                context.Markets.AddRange(markets);
                context.SaveChanges();
            }

            if (!context.CompanyMarketPrices.Any())
            {
                var company1 = context.Companies.FirstOrDefault(c => c.Id == 1);
                var company2 = context.Companies.FirstOrDefault(c => c.Id == 2);
                var market1 = context.Markets.FirstOrDefault(m => m.Id == 1);
                var market2 = context.Markets.FirstOrDefault(m => m.Id == 2);

                if (company1 == null || company2 == null || market1 == null || market2 == null)
                {
                    throw new Exception("One or more required entities (companies or markets) are missing from the database.");
                }

                var prices = new List<CompanyMarketPrice>
                {
                    new() { Id = 1, CompanyId = 1, MarketId = 1, Price = 100, Company = company1, Market = market1 },
                    new() { Id = 2, CompanyId = 1, MarketId = 2, Price = 120, Company = company1, Market = market2 },
                    new() { Id = 3, CompanyId = 2, MarketId = 1, Price = 90,  Company = company2, Market = market1 },
                    new() { Id = 4, CompanyId = 2, MarketId = 2, Price = 110, Company = company2, Market = market2 }
                };

                context.CompanyMarketPrices.AddRange(prices);
                context.SaveChanges();
            }
        }
    }
}
