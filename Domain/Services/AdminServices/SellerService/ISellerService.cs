using Domain.DTO.AdminDtos;
using Domain.Helpers;

namespace Domain.Services.AdminServices.SellerService
{
    public interface ISellerService
    {
        QueryResult<SellerDto> GetList(SearchCriteria model);
        Task<bool> Create(SellerDto input, string userId);
        SellerDto GetById(int Id);
        Task<bool> Edit(SellerDto input, string userId);
        bool Delete(int Id, string userId);
        List<SellerDto> GetDDL();
    }
}
