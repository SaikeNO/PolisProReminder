using MediatR;

namespace PolisProReminder.Application.Users.Commands.UnlockAssistant;

public class UnlockAssistantCommand : IRequest
{
    public Guid AssistantId { get; set; }
}
