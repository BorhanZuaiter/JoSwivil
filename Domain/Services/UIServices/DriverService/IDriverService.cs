using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.SellerService
{
    public interface IDriverService
    {
        List<DriverDto> GetDrivers();
        DriverDto GetById(int id);
        List<TripsDto> GetRTrips(int SellerId);
    }
}
