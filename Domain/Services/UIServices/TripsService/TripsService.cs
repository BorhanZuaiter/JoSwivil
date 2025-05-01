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
        public List<TripsDto> GetTrips()
        {
            var List = _db.Trips
                .Where(r => r.IsDeleted == false)
                .OrderBy(x => Guid.NewGuid())
                .Select(a => new TripsDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    DriverName = a.Driver.Name,
                    PlateNumber = a.Driver.PlateNumber,
                    Drivernumber = a.Driver.PhoneNumber,
                    DriverProfileImageUrl = a.Driver.ProfileImageUrl,
                    RouteName = a.Route.Name,
                    NoOfSeats = a.NoOfSeats,
                    Date = a.Date,
                    Price = a.Route.Price,
                    IsBooked = a.IsBooked,
                })
                .ToList();
            return List;
        }
        public bool ReserveTrip(int Id, string userId)
        {
            var obj = _db.Trips.Where(r => r.Id == Id).FirstOrDefault();
            obj.IsBooked = true;
            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.Trips.Update(obj);
            return _db.SaveChanges() > 0;
        }
        public bool Cancel(int Id, string userId)
        {
            var obj = _db.Trips.Where(r => r.Id == Id).FirstOrDefault();
            obj.IsBooked = false;
            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.Trips.Update(obj);
            return _db.SaveChanges() > 0;
        }
    }
}
