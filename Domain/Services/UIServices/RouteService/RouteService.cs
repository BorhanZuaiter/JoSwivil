using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.CategoryService
{
    public class RouteService : IRouteService
    {
        private readonly VODContext _db;

        public RouteService(VODContext db)
        {
            _db = db;
        }
        public List<RouteDto> GetHomeRoutes()
        {
            var List = _db.Route
                .Where(r => r.IsDeleted == false)
                .OrderBy(x => Guid.NewGuid())
                .Select(a => new RouteDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    PrivewImageUrl = a.PrivewImageUrl,
                })
                .Take(10)
                .ToList();
            return List;

        }
        public List<RouteDto> GetRoute()
        {
            var List = _db.Route.OrderByDescending(x => x.Id)
                .Where(r => r.IsDeleted == false).Select(a => new RouteDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    PrivewImageUrl = a.PrivewImageUrl,
                }).ToList();

            return List;
        }
        public List<TripsDto> GetTrips(int RouteId)
        {
            var List = _db.Trips.OrderByDescending(x => x.Id)
                .Where(r => r.IsDeleted == false && r.RouteId == RouteId).Select(a => new TripsDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    RouteName = a.Route.Name,
                    DriverName = a.Driver.Name,
                }).ToList();

            return List;
        }
    }
}
