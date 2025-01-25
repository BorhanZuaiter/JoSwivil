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
        [HttpGet("Category/CategoryAuctions/{CategoryId}")]
        public IActionResult CategoryAuctions(int CategoryId)
        {
            var res = _service.GetAuctions(CategoryId);

            return View(res);
        }
    }
}
