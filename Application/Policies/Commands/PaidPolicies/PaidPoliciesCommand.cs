using MediatR;

namespace PolisProReminder.Application.Policies.Commands.PaidPolicies;

public class PaidPoliciesCommand : IRequest
{
    public IEnumerable<Guid> Ids { get; set; } = [];
}
