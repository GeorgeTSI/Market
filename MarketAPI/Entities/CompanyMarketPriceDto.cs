namespace MarketAPI.Entities
{
    public class CompanyMarketPriceDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public int MarketId { get; set; }
        public string MarketName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
