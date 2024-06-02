using MediatR;

namespace PolisProReminder.Application.Users.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommand : IRequest
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
}
