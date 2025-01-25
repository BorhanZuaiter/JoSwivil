using Domain.Services.UIServices.NewsService;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _service;

        public NewsController(INewsService service)
        {
            _service = service;
        }
        public IActionResult Index(int pageIndex = 1, int pageSize = 10)
        {
            var res = _service.GetNews();

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
