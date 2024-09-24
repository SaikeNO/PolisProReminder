using MediatR;
using PolisProReminder.Application.Users;
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

        insurer.Email = request.Email;
        insurer.Pesel = request.Pesel;
        insurer.PhoneNumber = request.PhoneNumber;
        insurer.FirstName = request.FirstName;
        insurer.LastName = request.LastName;
        insurer.PostalCode = request.PostalCode;
        insurer.City = request.City;
        insurer.Street = request.Street;

        await insurersRepository.SaveChanges();
    }
}
