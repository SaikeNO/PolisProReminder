using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies.Commands.PaidPolicies;

internal class PaidPoliciesCommandHandler(IUserContext userContext,
    IPoliciesRepository policiesRepository) : IRequestHandler<PaidPoliciesCommand>
{
    public async Task Handle(PaidPoliciesCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser() ?? throw new InvalidOperationException("Current User is not present");
        var policies = await policiesRepository.GetByIds(currentUser.AgentId, request.Ids);

        policies.ToList().ForEach(p => p.IsPaid = true);

        await policiesRepository.SaveChanges();
    }
}
