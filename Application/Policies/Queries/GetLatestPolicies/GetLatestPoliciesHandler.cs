using AutoMapper;
using MediatR;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies.Queries.GetLatestPolicies;

public class GetLatestPoliciesHandler(IUserContext userContext,
    IPoliciesRepository policiesRepository, 
    IMapper mapper) : IRequestHandler<GetLatestPoliciesQuery, IEnumerable<PolicyDto>>
{
    public async Task<IEnumerable<PolicyDto>> Handle(GetLatestPoliciesQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var policies = await policiesRepository.GetLatestPolicies(currentUser.AgentId, request.Count);

        return mapper.Map<IEnumerable<PolicyDto>>(policies);
    }
}
