using AutoMapper;
using MediatR;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Insurers.Queries.GetAllInsurers;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Queries.GetInsurers;

public class GetPaginatedInsurersHandler(IInsurersRepository insurersRepository, 
    IMapper mapper,
    IUserContext userContext) : IRequestHandler<GetPaginatedInsurersQuery, PageResult<InsurerDto>>
{
    public async Task<PageResult<InsurerDto>> Handle(GetPaginatedInsurersQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var (insurers, totalCount) = await insurersRepository.GetAllMatchingAsync(currentUser.AgentId, 
            request.SearchPhrase,
            request.PageSize,
            request.PageIndex,
            request.SortBy,
            request.SortDirection);

        var insurersDtos = mapper.Map<List<InsurerDto>>(insurers);

        var result = new PageResult<InsurerDto>(insurersDtos, totalCount, request.PageSize, request.PageIndex);
        return result;
    }
}
