using MediatR;
using PolisProReminder.Application.Policies.Commands.BasePolicy;

namespace PolisProReminder.Application.Policies.Commands.UpdatePolicy;

public class UpdatePolicyCommand : BasePolicyCommand, IRequest
{
    public Guid Id { get; set; }
}
