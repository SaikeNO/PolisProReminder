using MediatR;

namespace PolisProReminder.Application.Users.Commands.CreateAssistant;

public class CreateAssistantCommand : IRequest<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
}
