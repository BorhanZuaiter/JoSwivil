using Domain.DTO.AdminDtos;
using Domain.Entities;
using Domain.Helpers;

namespace Domain.Services.AdminServices.SellerService
{
    public class SellerService : ISellerService
    {
        private readonly VODContext _db;
        private readonly StorageConnection _storageConnection;

        public SellerService(VODContext db, StorageConnection storageConnection)
        {
            _db = db;
            _storageConnection = storageConnection;
        }
        public QueryResult<SellerDto> GetList(SearchCriteria model)
        {
            var res = _db.Sellers
                .WhereIf(!string.IsNullOrEmpty(model.Search), r => r.Name.Contains(model.Search))
                .Where(r => r.IsDeleted == false)
                .OrderByDescending(r => r.Id)
                .Select(r => new SellerDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Bio = r.Bio,
                    Email = r.Email,
                }).ToQueryResult(model.PageNumber, model.PageSize);
            return res;
        }
        public async Task<bool> Create(SellerDto input, string userId)
        {
            var obj = new Seller();
            obj.Name = input.Name;
            obj.Email = input.Email;
            obj.Bio = input.Bio;
            obj.Address = input.Address;
            obj.NumberOfItems = input.NumberOfItems;
            obj.PhoneNumber = input.PhoneNumber;

            if (input.ProfileImage is not null)
                obj.ProfileImageUrl = await _storageConnection.SaveOnStorage(input.ProfileImage, "Sellers");

            if (input.CoverImage is not null)
                obj.CoverImageUrl = await _storageConnection.SaveOnStorage(input.CoverImage, "Sellers");

            if (input.IdImage is not null)
                obj.IdImageUrl = await _storageConnection.SaveOnStorage(input.IdImage, "Sellers");

            obj.CreatedBy = userId;
            obj.CreatedOn = DateTime.Now;
            _db.Sellers.Add(obj);
            return _db.SaveChanges() > 0;
        }
        public SellerDto GetById(int Id)
        {
            var obj = _db.Sellers.Where(r => r.Id == Id).Select(a => new SellerDto
            {
                Id = a.Id,
                Name = a.Name,
                Email = a.Email,
                Bio = a.Bio,
                Address = a.Address,
                PhoneNumber = a.PhoneNumber,
                NumberOfItems = a.NumberOfItems,
                IdImageUrl = a.IdImageUrl,
                CoverImageUrl = a.CoverImageUrl,
                ProfileImageUrl = a.ProfileImageUrl,

            }).FirstOrDefault();
            return obj;
        }
        public async Task<bool> Edit(SellerDto input, string userId)
        {
            var obj = _db.Sellers.Where(r => r.Id == input.Id).FirstOrDefault();
            obj.Name = input.Name;
            obj.Email = input.Email;
            obj.Bio = input.Bio;
            obj.Address = input.Address;
            obj.NumberOfItems = input.NumberOfItems;
            obj.PhoneNumber = input.PhoneNumber;

            if (input.ProfileImage is not null)
                obj.ProfileImageUrl = await _storageConnection.SaveOnStorage(input.ProfileImage, "Sellers");

            if (input.CoverImage is not null)
                obj.CoverImageUrl = await _storageConnection.SaveOnStorage(input.CoverImage, "Sellers");

            if (input.IdImage is not null)
                obj.IdImageUrl = await _storageConnection.SaveOnStorage(input.IdImage, "Sellers");

            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.Sellers.Update(obj);
            return _db.SaveChanges() > 0;
        }
        public bool Delete(int Id, string userId)
        {
            var obj = _db.Sellers.Where(r => r.Id == Id).FirstOrDefault();
            obj.IsDeleted = true;
            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.Sellers.Update(obj);
            return _db.SaveChanges() > 0;
        }
    }
}
