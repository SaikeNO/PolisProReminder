using AutoMapper;
using MediatR;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Commands.CreateInsurer;

public class CreateInsurerCommandHandler(IInsurersRepository insurersRepository, IMapper mapper) : IRequestHandler<CreateInsurerCommand, Guid>
{
    public async Task<Guid> Handle(CreateInsurerCommand request, CancellationToken cancellationToken)
    {
        if (await insurersRepository.GetByPeselAndId(request.Pesel, null) != null)
            throw new AlreadyExistsException("Klient o podanym numerze PESEL już istnieje");

        var insurer = mapper.Map<Insurer>(request);
        await insurersRepository.Create(insurer);

        return insurer.Id;
    }
}
