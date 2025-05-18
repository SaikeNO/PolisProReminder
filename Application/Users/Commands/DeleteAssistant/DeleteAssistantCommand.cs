using MediatR;

namespace PolisProReminder.Application.Users.Commands.DeleteAssistant;

public class DeleteAssistantCommand : IRequest
{
    public Guid AssistantId { get; set; }
}
