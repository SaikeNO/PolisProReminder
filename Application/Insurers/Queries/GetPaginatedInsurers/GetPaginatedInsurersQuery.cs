using MediatR;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Domain.Constants;

namespace PolisProReminder.Application.Insurers.Queries.GetPaginatedInsurers;

public class GetPaginatedInsurersQuery : IRequest<PageResult<IndividualInsurerDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
