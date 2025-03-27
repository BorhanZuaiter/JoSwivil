using Domain.DTO.AdminDtos;
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
                    DriverName = r.Driver.Name,
                    RouteName = r.Route.Name,
                }).ToQueryResult(model.PageNumber, model.PageSize);
            return res;
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
