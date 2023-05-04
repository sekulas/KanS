using System.Security.Claims;

namespace KanS.Interfaces;

public interface IUserContextService {
    int? GetUserId { get; }
    ClaimsPrincipal User { get; }
}