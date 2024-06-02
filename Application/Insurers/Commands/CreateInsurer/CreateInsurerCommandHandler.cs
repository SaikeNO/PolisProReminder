﻿using AutoMapper;
using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Commands.CreateInsurer;

public class CreateInsurerCommandHandler(IInsurersRepository insurersRepository, 
    IMapper mapper,
    IUserContext userContext) : IRequestHandler<CreateInsurerCommand, Guid>
{
    public async Task<Guid> Handle(CreateInsurerCommand request, CancellationToken cancellationToken)
    {
        if (await insurersRepository.GetByPeselAndId(request.Pesel, null) != null)
            throw new AlreadyExistsException("Klient o podanym numerze PESEL już istnieje");

        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var insurer = mapper.Map<Insurer>(request);

        insurer.CreatedByUserId = currentUser.Id;
        insurer.CreatedByAgentId = currentUser.SuperiorId ?? currentUser.Id;

        await insurersRepository.Create(insurer);

        return insurer.Id;
    }
}
