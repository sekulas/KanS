using System.Security.Claims;
using KanS.Interfaces;

namespace KanS.Services;

public class UserContextService : IUserContextService{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor) {
        _httpContextAccessor = httpContextAccessor;
    }
    public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;
    public int? GetUserId =>
        User == null ? null : (int?) int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
}
