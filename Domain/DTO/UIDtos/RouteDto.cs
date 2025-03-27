namespace Domain.DTO.UIDtos
{
    public class RouteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PrivewImageUrl { get; set; }
        public ICollection<TripsDto> Trips { get; set; }

    }
}
