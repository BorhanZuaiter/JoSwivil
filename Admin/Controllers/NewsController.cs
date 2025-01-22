using Domain.DTO.AdminDtos;
using Domain.Entities;
using Domain.Helpers;
using Domain.Services.AdminServices.NewsService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class NewsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly INewsService _service;

        public NewsController(UserManager<User> userManager, INewsService service)
        {
            _userManager = userManager;
            _service = service;
        }
        public IActionResult Index(SearchCriteria model)
        {
            var data = _service.GetList(model);
            var res = new TableDto<NewsDto>();
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
        public async Task<IActionResult> Create(NewsDto data)
        {
            var user = await _userManager.GetUserAsync(User);
            var res = await _service.Create(data, user.Id);
            if (res)
                return Json(new { status = true });
            return Json(new { status = false, message = "حدث خطأ ما" });
        }
        public IActionResult Edit(int Id)
        {
            var res = _service.GetById(Id);
            return View(res);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(NewsDto data)
        {
            var user = await _userManager.GetUserAsync(User);
            var res = await _service.Edit(data, user.Id);
            if (res)
                return Json(new { status = true });
            return Json(new { status = false, message = "حدث خطأ ما" });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var res = _service.Delete(id, user.Id);
            if (res)
                return Json(new { status = true });
            return Json(new { status = false, message = "حدث خطأ ما" });
        }
    }
}
