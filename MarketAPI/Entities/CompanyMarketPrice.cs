namespace MarketAPI.Entities
{
    public class CompanyMarketPrice
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public required Company Company { get; set; }
        public int MarketId { get; set; }
        public required Market Market { get; set; }
        public decimal Price { get; set; }
    }
}
