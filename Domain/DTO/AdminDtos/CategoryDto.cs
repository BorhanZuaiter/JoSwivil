using Microsoft.AspNetCore.Http;

namespace Domain.DTO.AdminDtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile PrivewImage { get; set; }
        public string PrivewImageUrl { get; set; }
        public ICollection<ItemDto> Items { get; set; }
    }
}
