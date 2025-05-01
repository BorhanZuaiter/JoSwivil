namespace Domain.DTO.UIDtos
{
    public class TripsDto
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int RouteId { get; set; }
        public int DriverId { get; set; }
        public string Name { get; set; }
        public string DriverName { get; set; }
        public int Drivernumber { get; set; }
        public string DriverProfileImageUrl { get; set; }
        public string PlateNumber { get; set; }
        public string RouteName { get; set; }
        public int NoOfSeats { get; set; }
        public DateTime Date { get; set; }
        public bool IsBooked { get; set; }
    }
}
