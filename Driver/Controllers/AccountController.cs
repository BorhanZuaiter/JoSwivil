using Domain;
using Domain.DTO.AdminDtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class AccountController : Controller
{
    private readonly VODContext _context;

    public AccountController(VODContext context)
    {
        _context = context;
    }

    public IActionResult Login()
    {
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

        var driver = await _context.Driver
            .FirstOrDefaultAsync(d => d.Email == input.Email);

        if (driver == null || driver.Password != input.Password)
        {
            ModelState.AddModelError(string.Empty, "الرجاء التأكد من البريد الالكتروني و كلمة المرور");
            return View();
        }

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, driver.Name),
        new Claim(ClaimTypes.Email, driver.Email),
        new Claim("DriverId", driver.Id.ToString())
    };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties { IsPersistent = true };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        return RedirectToAction("Index", "Home");
    }


    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
