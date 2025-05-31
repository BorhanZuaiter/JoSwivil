using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.AuctionService
{
    public interface ITripsService
    {
        List<TripsDto> GetTrips(string userId = null);
        Task<bool> ReserveTrip(int tripId, string userId);
        Task<bool> Cancel(int tripId, string userId);
    }
}
