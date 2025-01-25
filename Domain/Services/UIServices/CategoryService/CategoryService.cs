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
        public List<CategoryDto> GetHomecategories()
        {
            var List = _db.Categories
                .Where(r => r.IsDeleted == false)
                .OrderBy(x => Guid.NewGuid())
                .Select(a => new CategoryDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    PrivewImageUrl = a.PrivewImageUrl,
                })
                .Take(10)
                .ToList();
            return List;

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
        public List<ItemDto> GetAuctions(int CategoryId)
        {
            var List = _db.Items.OrderByDescending(x => x.Id)
                .Where(r => r.IsDeleted == false && r.CategoryId == CategoryId).Select(a => new ItemDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Sellername = a.Seller.Name,
                    Sellerimage = a.Seller.ProfileImageUrl,
                    PrivewImageUrl = a.PrivewImageUrl,
                    CurrentBiddingPrice = a.CurrentBiddingPrice,
                    SKU = a.SKU,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    CategoryId = a.CategoryId,
                }).ToList();

            return List;
        }
    }
}
