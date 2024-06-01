using MediatR;

namespace PolisProReminder.Application.Policies.Commands.DeletePolicy;

public class DeletePolicyCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
