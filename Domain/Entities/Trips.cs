namespace Domain.Entities
{
    public class Trips : AuditEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}
