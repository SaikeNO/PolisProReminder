using AutoMapper;
using MediatR;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Queries.GetPaginatedBusinessInsurers;

public class GetPaginatedBusinessInsurersHandler(IBusinessInsurersRepository insurersRepository,
    IMapper mapper,
    IUserContext userContext) : IRequestHandler<GetPaginatedBusinessInsurersQuery, PageResult<BusinessInsurerDto>>
{
    public async Task<PageResult<BusinessInsurerDto>> Handle(GetPaginatedBusinessInsurersQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var (insurers, totalCount) = await insurersRepository.GetAllMatchingAsync(currentUser.AgentId,
            request.SearchPhrase,
            request.PageSize,
            request.PageIndex,
            request.SortBy,
            request.SortDirection);

        var insurersDtos = mapper.Map<IEnumerable<BusinessInsurerDto>>(insurers);

        var result = new PageResult<BusinessInsurerDto>(insurersDtos, totalCount, request.PageSize, request.PageIndex);
        return result;
    }
}