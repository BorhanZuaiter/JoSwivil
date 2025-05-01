using Domain.DTO.AdminDtos;
using Domain.Helpers;

namespace Domain.Services.AdminServices.ItemService
{
    public interface ITripsService
    {
        QueryResult<TripsDto> GetList(SearchCriteria model);
        bool Delete(int Id, string userId);
        Task<bool> Create(TripsDto input, string userId);
        TripsDto GetById(int Id);
        Task<bool> Edit(TripsDto input, string userId);
    }
}
