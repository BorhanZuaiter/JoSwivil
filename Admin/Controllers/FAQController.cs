using Domain.DTO.AdminDtos;
using Domain.Entities;
using Domain.Helpers;
using Domain.Services.AdminServices.FAQService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class FAQController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IFAQService _fAQService;

        public FAQController(UserManager<User> userManager, IFAQService fAQService)
        {
            _userManager = userManager;
            _fAQService = fAQService;
        }
        public IActionResult Index(SearchCriteria model)
        {
            var data = _fAQService.GetList(model);
            var res = new TableDto<FAQDto>();
            res.DataList = data.Items.ToList();
            res.TotalCount = (int)Math.Ceiling(Convert.ToDouble(data.TotalCount) / model.PageSize);
            res.PageNumber = model.PageNumber;
            res.Search = model.Search;
            return View(res);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FAQDto data)
        {
            var user = await _userManager.GetUserAsync(User);
            var res = await _fAQService.Create(data, user.Id);
            if (res)
                return Json(new { status = true });
            return Json(new { status = false, message = "حدث خطأ ما" });
        }
        public IActionResult Edit(int Id)
        {
            var res = _fAQService.GetById(Id);
            return View(res);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(FAQDto data)
        {
            var user = await _userManager.GetUserAsync(User);
            var res = await _fAQService.Edit(data, user.Id);
            if (res)
                return Json(new { status = true });
            return Json(new { status = false, message = "حدث خطأ ما" });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var res = _fAQService.Delete(id, user.Id);
            if (res)
                return Json(new { status = true });
            return Json(new { status = false, message = "حدث خطأ ما" });
        }
    }
}
