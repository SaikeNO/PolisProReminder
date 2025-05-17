using MediatR;

namespace PolisProReminder.Application.Users.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommand : IRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
