using MediatR;
using PolisProReminder.Common;
using PolisProReminder.Models;

namespace PolisProReminder.Queries.GetAllPolicies;

public class GetAllPoliciesQuery : IRequest<PageResult<PolicyDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
