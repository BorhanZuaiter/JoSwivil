namespace Domain.DTO.AdminDtos
{
    public class TripsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RouteId { get; set; }
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public string RouteName { get; set; }
    }
}
