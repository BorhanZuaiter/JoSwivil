using Domain.DTO.UIDtos;
using Domain.Services.UIServices.SellerService;

namespace Domain.Services.UIServices.Driverervice
{
    public class DriverService : IDriverService
    {
        private readonly VODContext _db;

        public DriverService(VODContext db)
        {
            _db = db;
        }
        public List<DriverDto> GetDrivers()
        {
            var List = _db.Driver.OrderByDescending(x => x.Id)
                .Where(r => r.IsDeleted == false).Select(a => new DriverDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    PhoneNumber = a.PhoneNumber,
                    Address = a.Address,
                    ProfileImageUrl = a.ProfileImageUrl,
                }).ToList();

            return List;
        }
        public DriverDto GetById(int id)
        {
            var res = _db.Driver.Where(r => r.IsDeleted == false && r.Id == id).Select(a => new DriverDto
            {
                Id = a.Id,
                Name = a.Name,
                PhoneNumber = a.PhoneNumber,
                Address = a.Address,
                ProfileImageUrl = a.ProfileImageUrl,
                IdImageUrl = a.IdImageUrl,
                Email = a.Email,
                Trips = a.Trips.Where(s => !s.IsDeleted).Select(s => new TripsDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    DriverName = s.Driver.Name,
                    RouteName = s.Route.Name,
                }).ToList()

            }).FirstOrDefault();
            return res;
        }
        public List<TripsDto> GetRTrips(int SellerId)
        {
            var List = _db.Route.OrderByDescending(x => x.Id)
                .Where(r => r.IsDeleted == false && r.Id == SellerId).Select(a => new TripsDto
                {
                    Id = a.Id,
                    Name = a.Name,
                }).ToList();
            return List;
        }
    }
}
