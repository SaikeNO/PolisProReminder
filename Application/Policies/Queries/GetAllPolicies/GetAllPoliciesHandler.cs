using AutoMapper;
using MediatR;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies.Queries.GetAllPolicies;

public class GetAllPoliciesHandler(IPoliciesRepository policiesRepository, IMapper mapper) : IRequestHandler<GetAllPoliciesQuery, PageResult<PolicyDto>>
{
    public async Task<PageResult<PolicyDto>> Handle(GetAllPoliciesQuery request, CancellationToken cancellationToken)
    {
        var (policies, totalCount) = await policiesRepository.GetAllMatchingAsync(request.SearchPhrase,
            request.PageSize,
            request.PageIndex,
            request.SortBy,
            request.SortDirection,
            request.TypeId);

        var policiesDtos = mapper.Map<List<PolicyDto>>(policies);

        var result = new PageResult<PolicyDto>(policiesDtos, totalCount, request.PageSize, request.PageIndex);
        return result;
    }
}
