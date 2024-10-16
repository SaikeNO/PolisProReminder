using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Commands.UpdateBusinessInsurer;

internal class UpdateBusinessInsurerCommandHandler(IBusinessInsurersRepository insurersRepository, IUserContext userContext) : IRequestHandler<UpdateBusinessInsurerCommand>
{
    public async Task Handle(UpdateBusinessInsurerCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var insurer = await insurersRepository.GetById(currentUser.AgentId, request.Id);
        _ = insurer ?? throw new NotFoundException("Klient o podanym ID nie istnieje");

        if (await insurersRepository.GetByNipRegonAndId(currentUser.AgentId, request.Nip, request.Regon, request.Id) != null)
            throw new AlreadyExistsException("Klient o podanym numerze NIP lub REGON już istnieje");

        if (insurer is BusinessInsurer individualInsurer)
        {
            individualInsurer.Update(request.PhoneNumber, request.Email, request.PostalCode, request.City, request.Street);
            individualInsurer.Update(request.Name, request.Nip, request.Regon);
        }
        else
        {
            throw new InvalidOperationException("Klient nie jest typu biznesowego");
        }

        await insurersRepository.SaveChanges();
    }
}