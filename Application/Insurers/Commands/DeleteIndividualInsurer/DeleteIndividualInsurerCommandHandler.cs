using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Commands.DeleteIndividualInsurer;

public class DeleteIndividualInsurerCommandHandler(IIndividualInsurersRepository insurersRepository,
    IUserContext userContext) : IRequestHandler<DeleteIndividualInsurerCommand>
{
    public async Task Handle(DeleteIndividualInsurerCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var insurer = await insurersRepository.GetById(currentUser.AgentId, request.Id);

        _ = insurer ?? throw new NotFoundException("Klient o podanym ID nie istnieje");

        if (insurer.Policies.ToList().Count != 0)
            throw new NotAllowedException("Klient posiada polisy");

        insurer.Delete();

        await insurersRepository.SaveChanges();
    }
}
