using Domain.Entities;
using Domain.Services.UIServices.AccountService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<User> _userManager;

        public AccountController(IAccountService accountService, UserManager<User> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;

        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (await _accountService.RegisterUserAsync(model))
            {
                TempData["RegistrationSuccess"] = "You have been registered successfully! Redirecting to login...";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "Registration failed");
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _accountService.LoginUserAsync(model);
            if (result.Status)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", result.Message);
            return View(model);

        }
        public IActionResult DashboardChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View("DashboardChangePassword", model);

            var userId = _userManager.GetUserId(User); // Get the currently logged-in user's ID
            var result = await _accountService.ChangePasswordAsync(userId, model.CurrentPassword, model.NewPassword);

            if (result.Success)
            {
                ViewBag.Message = "Password changed successfully.";
                return RedirectToAction("DashboardChangePassword");
            }

            ModelState.AddModelError("", result.Message);
            return View("DashboardChangePassword", model);
        }
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutUserAsync();
            return RedirectToAction("Login");
        }
        public IActionResult DashboardMyAuctions()
        {
            return View();
        }
    }
}
