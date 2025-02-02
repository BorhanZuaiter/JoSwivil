using Domain.Services.UIServices.AuctionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace UI.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IAuctionService _service;
        private readonly IHubContext<AuctionHub> _hubContext;

        public AuctionController(IAuctionService service, IHubContext<AuctionHub> hubContext)
        {
            _service = service;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            var res = _service.GetAuctions();
            return View(res);
        }

        public IActionResult Details(int id)
        {
            var data = _service.GetById(id);
            if (data == null)
                return RedirectToAction("Index", "Home");
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceBid(int itemId, double bidAmount)
        {
            var success = _service.UpdateBid(itemId, bidAmount);
            if (success)
            {
                // Notify all users about the updated bid via SignalR
                await _hubContext.Clients.All.SendAsync("ReceiveBidUpdate", itemId, bidAmount);
                return Ok(new { success = true, newBid = bidAmount });
            }
            return BadRequest(new { success = false, message = "Bid placement failed" });
        }
    }
}
