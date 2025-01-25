namespace Domain.DTO.UIDtos
{
    public class HomeDto
    {
        public HomeDto()
        {
            Auction = new List<ItemDto>();
            Category = new List<CategoryDto>();
        }
        public ICollection<ItemDto> Auction { get; set; }
        public ICollection<CategoryDto> Category { get; set; }
    }
}
