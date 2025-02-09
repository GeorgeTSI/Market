using Microsoft.EntityFrameworkCore;
using MarketAPI.Data;
using MarketAPI.SignalRHub;
using Microsoft.AspNetCore.SignalR;
using MarketAPI.Entities;

namespace MarketAPI.Services
{
    public class PriceUpdateBackgroundService(IServiceScopeFactory serviceScopeFactory, IHubContext<PriceHub> hubContext) : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
        private readonly IHubContext<PriceHub> _hubContext = hubContext;
        private readonly Random _random = new();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    //var companyPrices = await context.CompanyMarketPrices.ToListAsync(cancellationToken: stoppingToken);


                    var companyPrices = await context.CompanyMarketPrices
                                        .Include(p => p.Company) 
                                        .Include(p => p.Market) 
                                        .ToListAsync(cancellationToken: stoppingToken);

                    foreach (var price in companyPrices)
                    {
                        var fluctuation = _random.NextDouble() * 0.1 - 0.05;
                        price.Price += price.Price * (decimal)fluctuation;
                    }

                    await context.SaveChangesAsync();

                    foreach (var price in companyPrices)
                    {
                        var priceDto = new CompanyMarketPriceDto
                        {
                            Id = price.Id,
                            CompanyId = price.CompanyId,
                            CompanyName = price.Company?.Name ?? "Company",
                            MarketId = price.MarketId,
                            MarketName = price.Market?.Name ?? "Market",
                            Price = price.Price
                        };

                        await _hubContext.Clients.All.SendAsync("ReceivePriceUpdate", priceDto, cancellationToken: stoppingToken);
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
