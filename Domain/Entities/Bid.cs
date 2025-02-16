namespace Domain.Entities
{
    internal class Bid
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public string UserId { get; set; } // Track the user who placed the bid
        public ApplicationUser User { get; set; }
        public double Amount { get; set; }
        public DateTime BidTime { get; set; } = DateTime.UtcNow;
    }
}
