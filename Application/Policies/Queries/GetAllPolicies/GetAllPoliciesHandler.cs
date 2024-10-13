using AutoMapper;
using MediatR;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies.Queries.GetAllPolicies;

public class GetAllPoliciesHandler(IUserContext userContext,
    IPoliciesRepository policiesRepository, 
    IMapper mapper) : IRequestHandler<GetAllPoliciesQuery, PageResult<PolicyDto>>
{
    public async Task<PageResult<PolicyDto>> Handle(GetAllPoliciesQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var (policies, totalCount) = await policiesRepository.GetAllMatchingAsync(currentUser.AgentId,
            request.SearchPhrase,
            request.PageSize,
            request.PageIndex,
            request.SortBy,
            request.SortDirection,
            request.TypeId,
            request.IsArchived);

        var policiesDtos = mapper.Map<IEnumerable<PolicyDto>>(policies);

        var result = new PageResult<PolicyDto>(policiesDtos, totalCount, request.PageSize, request.PageIndex);
        return result;
    }
}
