using Domain.Services.UIServices.SellerService;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class SellerController : Controller
    {
        private readonly ISellerService _service;

        public SellerController(ISellerService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var res = _service.GetCategory();

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
