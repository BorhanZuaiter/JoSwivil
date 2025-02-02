using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.AuctionService
{
    public interface IAuctionService
    {
        List<ItemDto> GetAuctions();
        ItemDto GetById(int id);
        List<ItemDto> GetHomeAuctions();
        bool UpdateBid(int itemId, double newBid);
    }
}
