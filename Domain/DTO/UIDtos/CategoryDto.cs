namespace Domain.DTO.UIDtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PrivewImageUrl { get; set; }
        public ICollection<ItemDto> Items { get; set; }

    }
}
