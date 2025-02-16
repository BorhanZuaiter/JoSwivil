using Domain.Entities;
using Domain.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services.UIServices.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly VODContext _db;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, VODContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public async Task<bool> RegisterUserAsync(RegisterViewModel model)
        {
            var user = new User
            {
                FullName = "User",
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper()  // Ensure email is stored properly
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            return result.Succeeded;
        }
        public async Task<Response> LoginUserAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
                return Response.Success();

            return Response.Failure("الرجاء التاكد من اسم المستخدم و كلمة المرور");
        }
        public async Task<(bool Success, string Message)> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return (false, "User not found.");

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result.Succeeded
                ? (true, "Password changed successfully.")
                : (false, string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
