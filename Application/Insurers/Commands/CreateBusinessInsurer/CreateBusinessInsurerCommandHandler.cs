using AutoMapper;
using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Commands.CreateBusinessInsurer;

internal class CreateBusinessInsurerCommandHandler(IBusinessInsurersRepository insurersRepository,
    IMapper mapper,
    IUserContext userContext) : IRequestHandler<CreateBusinessInsurerCommand, Guid>
{
    public async Task<Guid> Handle(CreateBusinessInsurerCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        if (await insurersRepository.GetByNipRegonAndId(currentUser.AgentId, request.Nip, request.Regon, null) != null)
            throw new AlreadyExistsException("Klient o podanym numerze NIP lub REGON już istnieje");

        var insurer = mapper.Map<BusinessInsurer>(request);

        insurer.CreatedByUserId = currentUser.Id;
        insurer.CreatedByAgentId = currentUser.AgentId;

        await insurersRepository.Create(insurer);

        return insurer.Id;
    }
}