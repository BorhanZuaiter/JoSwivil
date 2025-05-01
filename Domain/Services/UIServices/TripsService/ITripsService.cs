using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.AuctionService
{
    public interface ITripsService
    {
        List<TripsDto> GetTrips();
        bool ReserveTrip(int Id, string userId);
        bool Cancel(int Id, string userId);
    }
}
