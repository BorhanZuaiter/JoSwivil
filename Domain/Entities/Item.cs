namespace Domain.Entities
{
    public class Item : AuditEntity
    {
        public int Id { get; set; }
        public int NumberOfBids { get; set; }
        public double Price { get; set; }
        public double StartingPrice { get; set; }
        public double CurrentBiddingPrice { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public string Tags { get; set; }
        public string Weight { get; set; }
        public string Dimensions { get; set; }
        public string Brand { get; set; }
        public string PrivewImageUrl { get; set; }
        public string DetailsImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
