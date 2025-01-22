using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class FAQController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
