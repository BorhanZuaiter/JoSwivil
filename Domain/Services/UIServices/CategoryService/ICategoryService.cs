using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.CategoryService
{
    public interface ICategoryService
    {
        List<CategoryDto> GetCategory();
        List<ItemDto> GetAuctions(int CategoryId);
        List<CategoryDto> GetHomecategories();
    }
}
