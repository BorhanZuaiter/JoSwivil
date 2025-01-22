using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Domain.Common
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string UserId => _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        public string Name => GetNameFromCookies();
        private string GetNameFromCookies()
        {
            var name = _httpContextAccessor.HttpContext?.Request.Cookies["Name"] ?? "";
            return name;
        }
    }
}
