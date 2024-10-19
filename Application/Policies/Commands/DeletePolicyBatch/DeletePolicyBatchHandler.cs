using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies.Commands.DeletePolicyBatch;

internal class DeletePolicyBatchHandler(IUserContext userContext, IPoliciesRepository policiesRepository) : IRequestHandler<DeletePolicyBatchCommand>
{
    public async Task Handle(DeletePolicyBatchCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser() ?? throw new InvalidOperationException("Current User is not present");
        var policies = await policiesRepository.GetByIds(currentUser.AgentId, request.Ids);

        policies.ToList().ForEach(p => p.IsDeleted = true);

        await policiesRepository.SaveChanges();
    }
}
