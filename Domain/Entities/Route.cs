namespace Domain.Entities
{
    public class Route : AuditEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PrivewImageUrl { get; set; }

        public ICollection<Trips> Trips { get; set; }
    }
}
