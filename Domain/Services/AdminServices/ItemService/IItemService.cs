using Domain.DTO.AdminDtos;
using Domain.Helpers;

namespace Domain.Services.AdminServices.ItemService
{
    public interface IItemService
    {
        QueryResult<ItemDto> GetList(SearchCriteria model);
        Task<bool> Create(ItemDto input, string userId);
        ItemDto GetById(int Id);
        Task<bool> Edit(ItemDto input, string userId);
        bool Delete(int Id, string userId);
    }
}
