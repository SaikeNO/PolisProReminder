using MediatR;
using PolisProReminder.Application.Policies.Dtos;

namespace PolisProReminder.Application.Policies.Queries.GetPolicyById;

public class GetPolicyByIdQuery(Guid id) : IRequest<PolicyDto>
{
    public Guid Id { get; } = id;
}
