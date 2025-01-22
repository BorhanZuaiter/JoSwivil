namespace Domain.Entities
{
    public class FAQ : AuditEntity
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
