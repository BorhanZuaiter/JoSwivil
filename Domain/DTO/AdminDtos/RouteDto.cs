using Microsoft.AspNetCore.Http;

namespace Domain.DTO.AdminDtos
{
    public class RouteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public IFormFile PrivewImage { get; set; }
        public string PrivewImageUrl { get; set; }
        public ICollection<TripsDto> Trips { get; set; }
    }
}
