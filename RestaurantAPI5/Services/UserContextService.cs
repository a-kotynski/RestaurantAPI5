using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace RestaurantAPI5.Services
{
    public interface IUserContextService
    {
        int? GetUserId { get; }
        IHttpContextAccessor HttpContextAccessor { get; }
        ClaimsPrincipal User { get; }
    }

    public class UserContextService : IUserContextService
    {
        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IHttpContextAccessor HttpContextAccessor { get; }

        public ClaimsPrincipal User => HttpContextAccessor.HttpContext?.User;
        public int? GetUserId => User is null ? null : (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}
