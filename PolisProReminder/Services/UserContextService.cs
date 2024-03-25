using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace PolisProReminder.Services;

public interface IUserContextService
{
    int? GetUserId { get; }
    int? GetSuperiorId { get; }
    ClaimsPrincipal? User { get; }
}

public class UserContextService(IHttpContextAccessor httpContextAccessor) : IUserContextService
{
    public ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;
    public int? GetUserId
    {
        get
        {
            string? id = User?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return id is null ? null : int.Parse(id);
        }
    }
    public int? GetSuperiorId
    {
        get
        {
            var superiorId = User?.FindFirst(c => c.Type == "SuperiorId")?.Value;
            return superiorId.IsNullOrEmpty() ? null : int.Parse(superiorId!);
        }
    }
}
