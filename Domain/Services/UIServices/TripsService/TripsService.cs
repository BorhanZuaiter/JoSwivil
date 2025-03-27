using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.AuctionService
{
    public class TripsService : ITripsService
    {
        private readonly VODContext _db;

        public TripsService(VODContext db)
        {
            _db = db;
        }
        public List<TripsDto> GetHomeAuctions()
        {
            var List = _db.Trips
                .Where(r => r.IsDeleted == false)
                .OrderBy(x => Guid.NewGuid())
                .Select(a => new TripsDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    DriverName = a.Driver.Name,
                    RouteName = a.Route.Name,
                })
                .Take(10)
                .ToList();
            return List;

        }

        public List<TripsDto> GetAuctions()
        {
            var List = _db.Trips.OrderByDescending(x => x.Id)
                .Where(r => r.IsDeleted == false).Select(a => new TripsDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    DriverName = a.Driver.Name,
                    RouteName = a.Route.Name,
                }).ToList();

            return List;
        }
        public TripsDto GetById(int id)
        {
            var res = _db.Trips.Where(r => r.IsDeleted == false && r.Id == id).Select(a => new TripsDto
            {
                Id = a.Id,
                Name = a.Name,
                DriverName = a.Driver.Name,
                RouteName = a.Route.Name,

            }).FirstOrDefault();
            return res;
        }
    }
}
