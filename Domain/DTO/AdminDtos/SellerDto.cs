using Microsoft.AspNetCore.Http;

namespace Domain.DTO.AdminDtos
{
    public class SellerDto
    {
        public int Id { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string ProfileImageUrl { get; set; }
        public IFormFile CoverImage { get; set; }
        public string CoverImageUrl { get; set; }
        public IFormFile IdImage { get; set; }
        public string IdImageUrl { get; set; }
        public int NumberOfItems { get; set; }
        public int PhoneNumber { get; set; }
        public ICollection<ItemDto> Items { get; set; }
    }
}
