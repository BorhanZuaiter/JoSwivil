using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly VODContext _db;

        public CategoryService(VODContext db)
        {
            _db = db;
        }
        public List<CategoryDto> GetCategory()
        {
            var List = _db.Categories.OrderByDescending(x => x.Id)
                .Where(r => r.IsDeleted == false).Select(a => new CategoryDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    PrivewImageUrl = a.PrivewImageUrl,
                }).ToList();

            return List;
        }
        public CategoryDto GetById(int id)
        {
            var res = _db.Categories.Where(r => r.IsDeleted == false && r.Id == id).Select(a => new CategoryDto
            {
                Id = a.Id,
                Name = a.Name,
                PrivewImageUrl = a.PrivewImageUrl,
            }).FirstOrDefault();
            return res;
        }
    }
}
