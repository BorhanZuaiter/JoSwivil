namespace Domain.DTO.UIDtos
{
    public class SellerDto
    {
        public int Id { get; set; }
        public string Bio { get; set; }
        public string BioEn { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Address { get; set; }
        public string ProfileImage { get; set; }
        public string CoverImage { get; set; }
        public string IdImage { get; set; }
        public int NumberOfItems { get; set; }
        public int PhoneNumber { get; set; }
        public ICollection<ItemDto> Items { get; set; }
    }
}
