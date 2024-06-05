using AutoMapper;
using MediatR;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies.Queries.GetLatestPolicies;

public class GetLatestPoliciesHandler(IPoliciesRepository 
    policiesRepository, IMapper mapper) : IRequestHandler<GetLatestPoliciesQuery, IEnumerable<PolicyDto>>
{
    public async Task<IEnumerable<PolicyDto>> Handle(GetLatestPoliciesQuery request, CancellationToken cancellationToken)
    {
        var policies = await policiesRepository.GetLatestPolicies(request.Count);

        return mapper.Map<IEnumerable<PolicyDto>>(policies);
    }
}
