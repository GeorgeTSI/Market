namespace MarketAPI.Entities
{
    public class Market
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public required ICollection<CompanyMarketPrice> CompanyMarketPrices { get; set; } = [];
    }
}
