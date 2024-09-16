using MediatR;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Domain.Constants;

namespace PolisProReminder.Application.Policies.Queries.GetAllPolicies;

public class GetAllPoliciesQuery : IRequest<PageResult<PolicyDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
    public Guid? TypeId { get; set; }
    public bool IsArchived { get; set; }
}
