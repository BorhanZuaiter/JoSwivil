using Domain.Services.UIServices.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
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
