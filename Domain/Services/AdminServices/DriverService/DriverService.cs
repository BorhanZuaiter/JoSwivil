using Domain.DTO.AdminDtos;
using Domain.Entities;
using Domain.Helpers;
using Domain.Services.AdminServices.SellerService;

namespace Domain.Services.AdminServices.Driverervice
{
    public class DriverService : IDriverService
    {
        private readonly VODContext _db;
        private readonly StorageConnection _storageConnection;

        public DriverService(VODContext db, StorageConnection storageConnection)
        {
            _db = db;
            _storageConnection = storageConnection;
        }
        public QueryResult<DriverDto> GetList(SearchCriteria model)
        {
            var res = _db.Driver
                .WhereIf(!string.IsNullOrEmpty(model.Search), r => r.Name.Contains(model.Search))
                .Where(r => r.IsDeleted == false)
                .OrderByDescending(r => r.Id)
                .Select(r => new DriverDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Email = r.Email,
                    PhoneNumber = r.PhoneNumber,
                }).ToQueryResult(model.PageNumber, model.PageSize);
            return res;
        }
        public async Task<bool> Create(DriverDto input, string userId)
        {
            var obj = new Driver();
            obj.Name = input.Name;
            obj.Email = input.Email;
            obj.Address = input.Address;
            obj.PhoneNumber = input.PhoneNumber;
            obj.RouteId = input.RouteId;

            if (input.ProfileImage is not null)
                obj.ProfileImageUrl = await _storageConnection.SaveOnStorage(input.ProfileImage, "Driver");

            if (input.IdImage is not null)
                obj.IdImageUrl = await _storageConnection.SaveOnStorage(input.IdImage, "Driver");

            obj.CreatedBy = userId;
            obj.CreatedOn = DateTime.Now;
            _db.Driver.Add(obj);
            return _db.SaveChanges() > 0;
        }
        public DriverDto GetById(int Id)
        {
            var obj = _db.Driver.Where(r => r.Id == Id).Select(a => new DriverDto
            {
                Id = a.Id,
                Name = a.Name,
                Email = a.Email,
                Address = a.Address,
                PhoneNumber = a.PhoneNumber,
                IdImageUrl = a.IdImageUrl,
                ProfileImageUrl = a.ProfileImageUrl,
                RouteId = a.RouteId,

            }).FirstOrDefault();
            return obj;
        }
        public async Task<bool> Edit(DriverDto input, string userId)
        {
            var obj = _db.Driver.Where(r => r.Id == input.Id).FirstOrDefault();
            obj.Name = input.Name;
            obj.Email = input.Email;
            obj.Address = input.Address;
            obj.PhoneNumber = input.PhoneNumber;
            obj.RouteId = input.RouteId;

            if (input.ProfileImage is not null)
                obj.ProfileImageUrl = await _storageConnection.SaveOnStorage(input.ProfileImage, "Driver");

            if (input.IdImage is not null)
                obj.IdImageUrl = await _storageConnection.SaveOnStorage(input.IdImage, "Driver");

            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.Driver.Update(obj);
            return _db.SaveChanges() > 0;
        }
        public bool Delete(int Id, string userId)
        {
            var obj = _db.Driver.Where(r => r.Id == Id).FirstOrDefault();
            obj.IsDeleted = true;
            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.Driver.Update(obj);
            return _db.SaveChanges() > 0;
        }
        public List<DriverDto> GetDDL()
        {
            return _db.Driver.Where(r => r.IsDeleted == false).Select(a => new DriverDto { Id = a.Id, Name = a.Name }).ToList();
        }
    }
}
