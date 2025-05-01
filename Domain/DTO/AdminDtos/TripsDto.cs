namespace Domain.DTO.AdminDtos
{
    public class TripsDto
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int NoOfSeats { get; set; }
        public string Name { get; set; }
        public int RouteId { get; set; }
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public string RouteName { get; set; }
        public DateTime Date { get; set; }

    }
}
