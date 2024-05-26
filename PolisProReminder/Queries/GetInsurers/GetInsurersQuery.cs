using MediatR;
using PolisProReminder.Common;
using PolisProReminder.Models;

namespace PolisProReminder.Queries.GetInsurers;

public class GetInsurersQuery : IRequest<PageResult<InsurerDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
