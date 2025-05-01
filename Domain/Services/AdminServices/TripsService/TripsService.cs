using Domain.DTO.AdminDtos;
using Domain.Entities;
using Domain.Helpers;
using Domain.Services.AdminServices.ItemService;

namespace Domain.Services.AdminServices.Tripservice
{
    public class TripsService : ITripsService
    {
        private readonly VODContext _db;
        private readonly StorageConnection _storageConnection;

        public TripsService(VODContext db, StorageConnection storageConnection)
        {
            _db = db;
            _storageConnection = storageConnection;
        }
        public QueryResult<TripsDto> GetList(SearchCriteria model)
        {
            var res = _db.Trips
                .WhereIf(!string.IsNullOrEmpty(model.Search), r => r.Route.Name.Contains(model.Search) || r.Driver.Name.Contains(model.Search))
                .Where(r => r.IsDeleted == false)
                .OrderByDescending(r => r.Id)
                .Select(r => new TripsDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Date = r.Date,
                    DriverName = r.Driver.Name,
                    RouteName = r.Route.Name,
                    Price = r.Route.Price,
                    NoOfSeats = r.NoOfSeats,
                }).ToQueryResult(model.PageNumber, model.PageSize);
            return res;
        }
        public async Task<bool> Create(TripsDto input, string userId)
        {
            var obj = new Trips();
            obj.Name = input.Name;
            obj.Date = input.Date;
            obj.DriverId = input.DriverId;
            obj.RouteId = input.RouteId;
            obj.NoOfSeats = input.NoOfSeats;


            obj.CreatedBy = userId;
            obj.CreatedOn = DateTime.Now;
            _db.Trips.Add(obj);
            return _db.SaveChanges() > 0;
        }
        public TripsDto GetById(int Id)
        {
            var obj = _db.Trips.Where(r => r.Id == Id).Select(a => new TripsDto
            {
                Id = a.Id,
                Name = a.Name,
                Date = a.Date,
                NoOfSeats = a.NoOfSeats,
                DriverId = a.DriverId,
                RouteId = a.RouteId,

            }).FirstOrDefault();
            return obj;
        }
        public async Task<bool> Edit(TripsDto input, string userId)
        {
            var obj = _db.Trips.Where(r => r.Id == input.Id).FirstOrDefault();
            obj.Name = input.Name;
            obj.Date = input.Date;
            obj.DriverId = input.DriverId;
            obj.RouteId = input.RouteId;
            obj.NoOfSeats = input.NoOfSeats;

            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.Trips.Update(obj);
            return _db.SaveChanges() > 0;
        }
        public bool Delete(int Id, string userId)
        {
            var obj = _db.Trips.Where(r => r.Id == Id).FirstOrDefault();
            obj.IsDeleted = true;
            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.Trips.Update(obj);
            return _db.SaveChanges() > 0;
        }
    }
}
