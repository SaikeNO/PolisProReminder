using MediatR;
using PolisProReminder.Models;

namespace PolisProReminder.Queries.GetAllPolicies;

public class GetAllPoliciesQuery : IRequest<IEnumerable<PolicyDto>>
{
    public string? SearchPhrase { get; set; }
}
