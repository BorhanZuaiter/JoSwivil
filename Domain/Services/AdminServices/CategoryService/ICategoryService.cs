using Domain.DTO.AdminDtos;
using Domain.Helpers;

namespace Domain.Services.AdminServices.CategoryService
{
    public interface ICategoryService
    {
        QueryResult<CategoryDto> GetList(SearchCriteria model);
        Task<bool> Create(CategoryDto input, string userId);
        CategoryDto GetById(int Id);
        Task<bool> Edit(CategoryDto input, string userId);
        bool Delete(int Id, string userId);
        List<CategoryDto> GetDDL();
    }
}
