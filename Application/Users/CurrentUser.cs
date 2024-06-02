namespace PolisProReminder.Application.Users;

public record CurrentUser(string Id, string Email, string? SuperiorId, IEnumerable<string> Roles)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
