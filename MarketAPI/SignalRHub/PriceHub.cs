using MarketAPI.Entities;
using Microsoft.AspNetCore.SignalR;

namespace MarketAPI.SignalRHub
{
    public class PriceHub: Hub
    {
        public async Task SendPriceUpdate(CompanyMarketPriceDto updatedPrice)
        {
            await Clients.All.SendAsync("ReceivePriceUpdate", updatedPrice);
        }
    }
}
