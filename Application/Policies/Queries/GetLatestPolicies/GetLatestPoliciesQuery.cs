using MediatR;
using PolisProReminder.Application.Policies.Dtos;

namespace PolisProReminder.Application.Policies.Queries.GetLatestPolicies;

public class GetLatestPoliciesQuery(int count) : IRequest<IEnumerable<PolicyDto>>
{
    public int Count { get; } = count;
}
