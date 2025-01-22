namespace Domain.DTO.AdminDtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ItemDto> Items { get; set; }
    }
}
