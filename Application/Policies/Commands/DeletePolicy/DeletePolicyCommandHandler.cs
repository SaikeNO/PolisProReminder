using MediatR;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies.Commands.DeletePolicy;

public class DeletePolicyCommandHandler(IPoliciesRepository policiesRepository) : IRequestHandler<DeletePolicyCommand>
{
    public async Task Handle(DeletePolicyCommand request, CancellationToken cancellationToken)
    {
        var policy = await policiesRepository.GetById(request.Id);

        _ = policy ?? throw new NotFoundException("Polisa o podanym ID nie istnieje");

        await policiesRepository.Delete(policy);
    }
}
