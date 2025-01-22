namespace Domain.Entities
{
    public class Category : AuditEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Item> Items { get; set; }

    }
}
