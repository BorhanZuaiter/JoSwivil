using Domain;
using Domain.Common;
using Domain.Entities;
using Domain.Helpers;
using Domain.Services.AdminServices.CategoryService;
using Domain.Services.AdminServices.FAQService;
using Domain.Services.AdminServices.Feedbackervice;
using Domain.Services.AdminServices.FeedbackService;
using Domain.Services.AdminServices.ItemService;
using Domain.Services.AdminServices.NewsService;
using Domain.Services.AdminServices.SellerService;
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
builder.Services.AddTransient<IFAQService, FAQService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<ISellerService, SellerService>();
builder.Services.AddTransient<INewsService, NewsService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IFeedbackService, Feedbackervice>();
builder.Services.AddTransient<StorageConnection>();

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
