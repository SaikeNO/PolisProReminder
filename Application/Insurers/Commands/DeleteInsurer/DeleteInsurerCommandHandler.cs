﻿using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Commands.DeleteInsurer;

public class DeleteInsurerCommandHandler(IBaseInsurersRepository insurersRepository, IUserContext userContext) : IRequestHandler<DeleteInsurerCommand>
{
    public async Task Handle(DeleteInsurerCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var insurer = await insurersRepository.GetById(currentUser.AgentId, request.Id);

        _ = insurer ?? throw new NotFoundException("Klient o podanym ID nie istnieje");

        if (insurer.Policies.Where(p => !p.IsDeleted).ToList().Count != 0)
            throw new NotAllowedException("Klient posiada polisy");

        insurer.Delete();

        await insurersRepository.SaveChanges();
    }
}
