using Domain.DTO.AdminDtos;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Login()
        {
            //var newRole = new IdentityRole("Super Admin");
            //await _roleManager.CreateAsync(newRole);

            //var user1 = new User
            //{
            //    FullName = "superadmin",
            //    FirstName = "Super",
            //    LastName = "Admin",
            //    UserName = "SuperAdmin",
            //    Email = "SuperAdmin",
            //    PhoneNumber = "222222222222",
            //    IsDeleted = false,
            //    CreatedBy = "Admin",
            //    CreatedOn = DateTime.Now,
            //};
            //await _userManager.CreateAsync(user1, "123456");
            //await _userManager.AddToRoleAsync(user1, "Super Admin");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto input)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "الرجاء ادخال البريد الالكتروني و كلمة المرور");
                return View();
            }
            var user = await _userManager.FindByNameAsync(input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "الرجاء التأكد من البريد الالكتروني و كلمة المرور");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, false, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "الرجاء التأكد من البريد الالكتروني و كلمة المرور");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
