using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
