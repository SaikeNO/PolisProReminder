using MediatR;

namespace PolisProReminder.Application.Users.Commands.LockoutAssistant;

public class LockoutAssistantCommand : IRequest
{
    public Guid AssistantId { get; set; }
}
