using Domain.Services.UIServices.AuctionService;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IAuctionService _service;

        public AuctionController(IAuctionService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var res = _service.GetAuctions();

            return View(res);
        }
        public IActionResult Details(int id)
        {
            var data = _service.GetById(id);
            if (data == null)
                return RedirectToAction("Index", "Home");
            return View(data);
        }
    }
}
