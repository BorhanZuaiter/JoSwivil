using Domain.Entities;
using Domain.Helpers;

namespace Domain.Services.UIServices.AccountService
{
    public interface IAccountService
    {
        Task<bool> RegisterUserAsync(RegisterViewModel model);
        Task<Response> LoginUserAsync(LoginViewModel model);
        Task<(bool Success, string Message)> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task LogoutUserAsync();
    }
}
