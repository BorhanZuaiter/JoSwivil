using Domain.DTO.AdminDtos;
using Domain.Entities;
using Domain.Helpers;
using Domain.Services.AdminServices.FeedbackService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IFeedbackService _Service;

        public FeedbackController(UserManager<User> userManager, IFeedbackService service)
        {
            _userManager = userManager;
            _Service = service;
        }
        public IActionResult Index(SearchCriteria model)
        {
            var data = _Service.GetList(model);
            var res = new TableDto<FeedbackDto>();
            res.DataList = data.Items.ToList();
            res.TotalCount = (int)Math.Ceiling(Convert.ToDouble(data.TotalCount) / model.PageSize);
            res.PageNumber = model.PageNumber;
            res.Search = model.Search;
            return View(res);
        }
        [HttpPost]
        public async Task<IActionResult> SetContacted(int id)
        {
            var result = await _Service.SetContactedAsync(id);
            if (!result)
            {
                return Json(new { success = false, message = "Failed to update status." });
            }
            return Json(new { success = true });
        }
    }
}
