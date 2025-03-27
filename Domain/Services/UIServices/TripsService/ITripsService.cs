using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.AuctionService
{
    public interface ITripsService
    {
        List<TripsDto> GetAuctions();
        TripsDto GetById(int id);
        List<TripsDto> GetHomeAuctions();
    }
}
