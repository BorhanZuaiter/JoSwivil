using Domain.Services.UIServices.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class RouteController : Controller
    {
        private readonly IRouteService _service;

        public RouteController(IRouteService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var res = _service.GetRoute();

            return View(res);
        }
        [HttpGet("Category/CategoryAuctions/{CategoryId}")]
        public IActionResult CategoryAuctions(int RouteId)
        {
            var res = _service.GetTrips(RouteId);

            return View(res);
        }
    }
}
