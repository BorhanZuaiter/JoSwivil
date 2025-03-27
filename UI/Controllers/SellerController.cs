using Domain.Services.UIServices.SellerService;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class SellerController : Controller
    {
        private readonly IDriverService _service;

        public SellerController(IDriverService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var res = _service.GetDrivers();

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
