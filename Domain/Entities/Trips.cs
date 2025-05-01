namespace Domain.Entities
{
    public class Trips : AuditEntity
    {
        public int Id { get; set; }
        public int NoOfSeats { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsBooked { get; set; }
    }
}
