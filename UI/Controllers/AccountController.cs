using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult DashboardChangePassword()
        {
            return View();
        }
        public IActionResult DashboardMyAuctions()
        {
            return View();
        }
        public IActionResult DashboardPayment()
        {
            return View();
        }
        public IActionResult DashboardSettings()
        {
            return View();
        }
    }
}
