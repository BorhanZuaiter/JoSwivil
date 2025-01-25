using Domain.DTO.AdminDtos;
using Domain.Entities;
using Domain.Helpers;

namespace Domain.Services.AdminServices.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly VODContext _db;
        private readonly StorageConnection _storageConnection;

        public CategoryService(VODContext db, StorageConnection storageConnection)
        {
            _db = db;
            _storageConnection = storageConnection;
        }
        public QueryResult<CategoryDto> GetList(SearchCriteria model)
        {
            var res = _db.Categories
                .WhereIf(!string.IsNullOrEmpty(model.Search), r => r.Name.Contains(model.Search))
                .Where(r => r.IsDeleted == false)
                .OrderByDescending(r => r.Id)
                .Select(r => new CategoryDto
                {
                    Id = r.Id,
                    Name = r.Name,
                }).ToQueryResult(model.PageNumber, model.PageSize);
            return res;
        }
        public async Task<bool> Create(CategoryDto input, string userId)
        {
            var obj = new Category();
            obj.Name = input.Name;
            if (input.PrivewImage is not null)
                obj.PrivewImageUrl = await _storageConnection.SaveOnStorage(input.PrivewImage, "Category");

            obj.CreatedBy = userId;
            obj.CreatedOn = DateTime.Now;
            _db.Categories.Add(obj);
            return _db.SaveChanges() > 0;
        }
        public CategoryDto GetById(int Id)
        {
            var obj = _db.Categories.Where(r => r.Id == Id).Select(a => new CategoryDto
            {
                Id = a.Id,
                Name = a.Name,

            }).FirstOrDefault();
            return obj;
        }
        public async Task<bool> Edit(CategoryDto input, string userId)
        {
            var obj = _db.Categories.Where(r => r.Id == input.Id).FirstOrDefault();
            obj.Name = input.Name;
            if (input.PrivewImage is not null)
                obj.PrivewImageUrl = await _storageConnection.SaveOnStorage(input.PrivewImage, "Category");

            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.Categories.Update(obj);
            return _db.SaveChanges() > 0;
        }
        public bool Delete(int Id, string userId)
        {
            var obj = _db.Categories.Where(r => r.Id == Id).FirstOrDefault();
            obj.IsDeleted = true;
            obj.ModifiedBy = userId;
            obj.ModifiedOn = DateTime.Now;
            _db.Categories.Update(obj);
            return _db.SaveChanges() > 0;
        }
        public List<CategoryDto> GetDDL()
        {
            return _db.Categories.Where(r => r.IsDeleted == false).Select(a => new CategoryDto { Id = a.Id, Name = a.Name }).ToList();
        }
    }
}
