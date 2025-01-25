using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.SellerService
{
    public interface ISellerService
    {
        List<SellerDto> GetCategory();
        SellerDto GetById(int id);
        List<ItemDto> GetAuctions(int SellerId);
    }
}
