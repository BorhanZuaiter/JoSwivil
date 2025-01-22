namespace Domain.Entities
{
    public class News : AuditEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AutherName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string PrivewImageUrl { get; set; }
        public string DetailsImageUrl { get; set; }
    }
}
