namespace Domain.DTO.UIDtos
{
    public class HomeDto
    {
        public HomeDto()
        {
            Auction = new List<TripsDto>();
            Category = new List<RouteDto>();
        }
        public ICollection<TripsDto> Auction { get; set; }
        public ICollection<RouteDto> Category { get; set; }
    }
}
