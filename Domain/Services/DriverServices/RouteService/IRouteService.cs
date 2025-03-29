using Domain.DTO.AdminDtos;
using Domain.Helpers;

namespace Domain.Services.DriverServices.RouteService
{
    public interface IRouteService
    {
        QueryResult<RouteDto> GetList(SearchCriteria model);
        Task<bool> Create(RouteDto input, string userId);
        RouteDto GetById(int Id);
        Task<bool> Edit(RouteDto input, string userId);
        bool Delete(int Id, string userId);
        List<RouteDto> GetDDL();
    }
}
