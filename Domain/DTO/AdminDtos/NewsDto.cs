using Microsoft.AspNetCore.Http;

namespace Domain.DTO.AdminDtos
{
    public class NewsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AutherName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public IFormFile PrivewImage { get; set; }
        public string PrivewImageUrl { get; set; }
        public IFormFile DetailsImage { get; set; }
        public string DetailsImageUrl { get; set; }
    }
}
