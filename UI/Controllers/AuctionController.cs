using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class AuctionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}
