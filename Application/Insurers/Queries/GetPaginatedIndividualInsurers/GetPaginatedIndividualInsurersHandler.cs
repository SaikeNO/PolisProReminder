using AutoMapper;
using MediatR;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Queries.GetPaginatedIndividualInsurers;

public class GetPaginatedIndividualInsurersHandler(IIndividualInsurersRepository insurersRepository,
    IMapper mapper,
    IUserContext userContext) : IRequestHandler<GetPaginatedIndividualInsurersQuery, PageResult<IndividualInsurerDto>>
{
    public async Task<PageResult<IndividualInsurerDto>> Handle(GetPaginatedIndividualInsurersQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var (insurers, totalCount) = await insurersRepository.GetAllMatchingAsync(currentUser.AgentId,
            request.SearchPhrase,
            request.PageSize,
            request.PageIndex,
            request.SortBy,
            request.SortDirection);

        var insurersDtos = mapper.Map<IEnumerable<IndividualInsurerDto>>(insurers);

        var result = new PageResult<IndividualInsurerDto>(insurersDtos, totalCount, request.PageSize, request.PageIndex);
        return result;
    }
}
