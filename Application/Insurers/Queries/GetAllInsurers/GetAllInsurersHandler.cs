using AutoMapper;
using MediatR;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Insurers.Queries.GetAllInsurers;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Queries.GetInsurers;

public class GetAllInsurersHandler(IInsurersRepository insurersRepository, IMapper mapper) : IRequestHandler<GetAllInsurersQuery, PageResult<InsurerDto>>
{
    public async Task<PageResult<InsurerDto>> Handle(GetAllInsurersQuery request, CancellationToken cancellationToken)
    {
        var (insurers, totalCount) = await insurersRepository.GetAllMatchingAsync(request.SearchPhrase,
            request.PageSize,
            request.PageIndex,
            request.SortBy,
            request.SortDirection);

        var insurersDtos = mapper.Map<List<InsurerDto>>(insurers);

        var result = new PageResult<InsurerDto>(insurersDtos, totalCount, request.PageSize, request.PageIndex);
        return result;
    }
}
