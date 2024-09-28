namespace PolisProReminder.Application.Users;

public record CurrentUser(Guid Id, string Email, Guid AgentId, IEnumerable<string> Roles)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
