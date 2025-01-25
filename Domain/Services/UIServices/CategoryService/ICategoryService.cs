using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.CategoryService
{
    public interface ICategoryService
    {
        List<CategoryDto> GetCategory();
        CategoryDto GetById(int id);
    }
}
