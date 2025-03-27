using Domain;
using Domain.Common;
using Domain.Entities;
using Domain.Helpers;
using Domain.Services.UIServices.AccountService;
using Domain.Services.UIServices.AuctionService;
using Domain.Services.UIServices.CategoryService;
using Domain.Services.UIServices.Driverervice;
using Domain.Services.UIServices.HomeService;
using Domain.Services.UIServices.SellerService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VODContext>(options =>
    options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null
        )
    ));

// Add Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
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

// Add SignalR Service
builder.Services.AddSignalR();

// Register other services
builder.Services.AddTransient<IHomeService, HomeService>();
builder.Services.AddTransient<IRouteService, RouteService>();
builder.Services.AddTransient<IDriverService, DriverService>();
builder.Services.AddTransient<ITripsService, TripsService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddTransient<StorageConnection>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Map SignalR Hub
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
