using Domain.DTO.AdminDtos;
using Domain.Helpers;

namespace Domain.Services.DriverServices.DriverService
{
    public interface IDriverService
    {
        QueryResult<DriverDto> GetList(SearchCriteria model);
        Task<bool> Create(DriverDto input, string userId);
        DriverDto GetById(int Id);
        Task<bool> Edit(DriverDto input, string userId);
        bool Delete(int Id, string userId);
        List<DriverDto> GetDDL();
    }
}
