using Microsoft.AspNetCore.Http;

namespace Domain.DTO.UIDtos
{
    public class DriverDto
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public string Car { get; set; }
        public string PlateNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string ProfileImageUrl { get; set; }
        public IFormFile IdImage { get; set; }
        public string IdImageUrl { get; set; }
        public int PhoneNumber { get; set; }
        public ICollection<TripsDto> Trips { get; set; }
    }
}
