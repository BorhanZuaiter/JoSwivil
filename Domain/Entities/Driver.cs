namespace Domain.Entities
{
    public class Driver : AuditEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Car { get; set; }
        public string PlateNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ProfileImageUrl { get; set; }
        public string IdImageUrl { get; set; }
        public int PhoneNumber { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
        public string CreatedBy { get; set; }
        public ICollection<Trips> Trips { get; set; }

    }
}
