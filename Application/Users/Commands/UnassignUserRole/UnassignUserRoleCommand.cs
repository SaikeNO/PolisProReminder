using MediatR;

namespace PolisProReminder.Application.Users.Commands.UnassignUserRole;

public class UnassignUserRoleCommand : IRequest
{
    public string UserEmail { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}
