using Domain.DTO.AdminDtos;
using Domain.Entities;
using Domain.Helpers;
using Domain.Services.DriverServices.DriverService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Driver.Controllers
{
    public class DriverController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IDriverService _service;

        public DriverController(UserManager<User> userManager, IDriverService service)
        {
            _userManager = userManager;
            _service = service;
        }
        public IActionResult Index(SearchCriteria model)
        {
            var data = _service.GetList(model);
            var res = new TableDto<DriverDto>();
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
        public async Task<IActionResult> Create(DriverDto data)
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
        public async Task<IActionResult> Edit(DriverDto data)
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
        public JsonResult GetDDl()
        {
            return Json(_service.GetDDL());
        }
    }
}
