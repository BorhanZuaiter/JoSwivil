using Domain.DTO.AdminDtos;
using Domain.Entities;
using Domain.Helpers;

namespace Domain.Services.DriverServices.RouteService
{
    public class RouteService : IRouteService
    {
        private readonly VODContext _db;
        private readonly StorageConnection _storageConnection;

        public RouteService(VODContext db, StorageConnection storageConnection)
        {
            _db = db;
            _storageConnection = storageConnection;
        }
        public QueryResult<RouteDto> GetList(SearchCriteria model)
        {
            var res = _db.Route
                .WhereIf(!string.IsNullOrEmpty(model.Search), r => r.Name.Contains(model.Search))
                .Where(r => r.IsDeleted == false)
                .OrderByDescending(r => r.Id)
                .Select(r => new RouteDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Price = r.Price,
                }).ToQueryResult(model.PageNumber, model.PageSize);
            return res;
        }
        public async Task<bool> Create(RouteDto input, string userId)
        {
            var obj = new Route();
            obj.Name = input.Name;
            obj.Price = input.Price;
            if (input.PrivewImage is not null)
                obj.PrivewImageUrl = await _storageConnection.SaveOnStorage(input.PrivewImage, "Category");

            obj.CreatedBy = userId;
            obj.CreatedOn = DateTime.Now;
            _db.Route.Add(obj);
            return _db.SaveChanges() > 0;
        }
        public RouteDto GetById(int Id)
        {
            var obj = _db.Route.Where(r => r.Id == Id).Select(a => new RouteDto
            {
                Id = a.Id,
                Name = a.Name,
                Price = a.Price,

            }).FirstOrDefault();
            return obj;
        }
        public async Task<bool> Edit(RouteDto input, string userId)
        {
            var obj = _db.Route.Where(r => r.Id == input.Id).FirstOrDefault();
            obj.Name = input.Name;
            obj.Price = input.Price;
            if (input.PrivewImage is not null)
                obj.PrivewImageUrl = await _storageConnection.SaveOnStorage(input.PrivewImage, "Category");

            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.Route.Update(obj);
            return _db.SaveChanges() > 0;
        }
        public bool Delete(int Id, string userId)
        {
            var obj = _db.Route.Where(r => r.Id == Id).FirstOrDefault();
            obj.IsDeleted = true;
            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.Route.Update(obj);
            return _db.SaveChanges() > 0;
        }
        public List<RouteDto> GetDDL()
        {
            return _db.Route.Where(r => r.IsDeleted == false).Select(a => new RouteDto { Id = a.Id, Name = a.Name }).ToList();
        }
    }
}
