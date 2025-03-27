namespace Domain.DTO.UIDtos
{
    public class DriverDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ProfileImageUrl { get; set; }
        public string IdImageUrl { get; set; }
        public int PhoneNumber { get; set; }
        public ICollection<TripsDto> Trips { get; set; }
    }
}
