using Domain.DTO.AdminDtos;
using Domain.Helpers;

namespace Domain.Services.AdminServices.FAQService
{
    public interface IFAQService
    {
        QueryResult<FAQDto> GetList(SearchCriteria model);
        Task<bool> Create(FAQDto input, string userId);
        FAQDto GetById(int Id);
        Task<bool> Edit(FAQDto input, string userId);
        bool Delete(int Id, string userId);
    }
}
