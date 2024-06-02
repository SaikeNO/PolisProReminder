using MediatR;
using PolisProReminder.Application.Insurers.Commands.UpdateInurer;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Commands.UpdateInsurer;

public class UpdateInsurerCommandHandler(IInsurersRepository insurersRepository) : IRequestHandler<UpdateInsurerCommand>
{
    public async Task Handle(UpdateInsurerCommand request, CancellationToken cancellationToken)
    {
        var insurer = await insurersRepository.GetById(request.Id);

        _ = insurer ?? throw new NotFoundException("Klient o podanym ID nie istnieje");

        if (await insurersRepository.GetByPeselAndId(request.Pesel, request.Id) != null)
            throw new AlreadyExistsException("Klient o podanym numerze PESEL już istnieje");

        insurer.Email = request.Email;
        insurer.Pesel = request.Pesel;
        insurer.PhoneNumber = request.PhoneNumber;
        insurer.FirstName = request.FirstName;
        insurer.LastName = request.LastName;

        await insurersRepository.SaveChanges();
    }
}
