using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Commands.UpdateInsurer;

public class UpdateInsurerCommandHandler(IInsurersRepository insurersRepository,
    IUserContext userContext) : IRequestHandler<UpdateInsurerCommand>
{
    public async Task Handle(UpdateInsurerCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var insurer = await insurersRepository.GetById(currentUser.AgentId, request.Id);
        _ = insurer ?? throw new NotFoundException("Klient o podanym ID nie istnieje");

        if (await insurersRepository.GetByPeselAndId(currentUser.AgentId, request.Pesel, request.Id) != null)
            throw new AlreadyExistsException("Klient o podanym numerze PESEL już istnieje");

        insurer.Update(request.FirstName, request.LastName, request.PhoneNumber, request.Email, request.PostalCode, request.City, request.Street);
        insurer.Pesel = request.Pesel;



        await insurersRepository.SaveChanges();
    }
}
