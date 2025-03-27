using Domain.DTO.UIDtos;

namespace Domain.Services.UIServices.CategoryService
{
    public interface IRouteService
    {
        List<RouteDto> GetHomeRoutes();
        List<TripsDto> GetTrips(int RouteId);
        List<RouteDto> GetRoute();
    }
}
