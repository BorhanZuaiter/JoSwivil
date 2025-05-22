using Domain;
using Domain.Common;
using Domain.Entities;
using Domain.Helpers;
using Domain.Services.AdminServices.ItemService;
using Domain.Services.AdminServices.Tripservice;
using Domain.Services.DriverServices.DriverService;
using Domain.Services.DriverServices.RouteService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VODContext>(options =>
    options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null
        )
    ));

builder.Services.AddRazorPages();
builder.Services.AddIdentity<User, IdentityRole>(
options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;


})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<VODContext>()
.AddDefaultTokenProviders();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddTransient<IRouteService, RouteService>();
builder.Services.AddTransient<IDriverService, DriverService>();
builder.Services.AddTransient<ITripsService, TripsService>();
builder.Services.AddTransient<StorageConnection>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");


app.Run();
