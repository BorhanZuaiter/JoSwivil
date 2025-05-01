using Domain.DTO.UIDtos;
using Domain.Entities;
using Domain.Services.UIServices.AuctionService;
using Domain.Services.UIServices.CategoryService;
using Domain.Services.UIServices.HomeService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _service;
        private readonly ITripsService _auctionService;
        private readonly IRouteService _categoryService;
        private readonly ITripsService _tripsService;
        private readonly UserManager<User> _userManager;


        public HomeController(ILogger<HomeController> logger, IHomeService service, ITripsService auction, IRouteService category, ITripsService tripsService, UserManager<User> userManager)
        {
            _logger = logger;
            _service = service;
            _auctionService = auction;
            _categoryService = category;
            _tripsService = tripsService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var res = _tripsService.GetTrips();

            return View(res);
        }
        [HttpPut]
        public async Task<IActionResult> ReserveTrip(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var res = _tripsService.ReserveTrip(id, user.Id);
            if (res)
                return Json(new { status = true });
            return Json(new { status = false, message = "حدث خطأ ما" });
        }
        [HttpPut]
        public async Task<IActionResult> CancelReserveTrip(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var res = _tripsService.Cancel(id, user.Id);
            if (res)
                return Json(new { status = true });
            return Json(new { status = false, message = "حدث خطأ ما" });
        }
        public IActionResult FAQ()
        {
            var res = _service.GetFAQs();
            return View(res);
        }
        public IActionResult ContactUs()
        {
            return View(new FeedbackDto());
        }
        [HttpPost]
        public async Task<IActionResult> ContactUs(FeedbackDto data)
        {
            var result = await _service.InsertFeeback(data);
            if (result)
                return RedirectToAction("ContactUs");
            return View(data);
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult HowToSell()
        {
            return View();
        }
        public IActionResult HowToBid()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult TermsAndCondition()
        {
            return View();
        }
        public IActionResult ErrorPage()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
