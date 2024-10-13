using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Commands.UpdateIndividualInsurer;

public class UpdateIndividualInsurerCommandHandler(IIndividualInsurersRepository insurersRepository,
    IUserContext userContext) : IRequestHandler<UpdateIndividualInsurerCommand>
{
    public async Task Handle(UpdateIndividualInsurerCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var insurer = await insurersRepository.GetById(currentUser.AgentId, request.Id);
        _ = insurer ?? throw new NotFoundException("Klient o podanym ID nie istnieje");

        if (await insurersRepository.GetByPeselAndId(currentUser.AgentId, request.Pesel, request.Id) != null)
            throw new AlreadyExistsException("Klient o podanym numerze PESEL już istnieje");

        if (insurer is IndividualInsurer individualInsurer)
        {
            individualInsurer.Update(request.PhoneNumber, request.Email, request.PostalCode, request.City, request.Street);
            individualInsurer.Update(request.FirstName, request.LastName, request.Pesel);
        }
        else
        {
            throw new InvalidOperationException("Klient nie jest typu indywidualnego");
        }

        await insurersRepository.SaveChanges();
    }
}
