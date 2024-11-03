using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies.Commands.DeletePolicy;

public class DeletePolicyCommandHandler(IUserContext userContext, IPoliciesRepository policiesRepository) : IRequestHandler<DeletePolicyCommand>
{
    public async Task Handle(DeletePolicyCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser() ?? throw new InvalidOperationException("Current User is not present");
        var policy = await policiesRepository.GetById(currentUser.AgentId, request.Id) ?? throw new NotFoundException("Polisa o podanym ID nie istnieje");

        policiesRepository.Delete(policy);

        await policiesRepository.SaveChanges();
    }
}
