using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Vehicles.Commands.DeleteVehicle;

public class DeleteVehicleCommandHandler(IUserContext userContext,
    IVehiclesRepository vehiclesRepository,
    IAttachmentsRepository attachmentsRepository) : IRequestHandler<DeleteVehicleCommand>
{
    public async Task Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser() ?? throw new InvalidOperationException("Current User is not present");
        var vehicle = await vehiclesRepository.GetById(currentUser.AgentId, request.Id) ?? throw new NotFoundException("Pojazd o podanym numerze rejestracyjnym nie istnieje");

        await vehiclesRepository.Delete(vehicle);

        var attachments = (await attachmentsRepository.GetAll<Vehicle>(request.Id))!.ToList();

        attachments.ForEach(attachment => attachmentsRepository.Delete(attachment));

        await vehiclesRepository.SaveChanges();
    }
}
