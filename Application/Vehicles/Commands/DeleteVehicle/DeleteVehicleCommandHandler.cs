using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Vehicles.Commands.DeleteVehicle;

public class DeleteVehicleCommandHandler(IUserContext userContext,
    IVehiclesRepository vehiclesRepository) : IRequestHandler<DeleteVehicleCommand>
{
    public async Task Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser() ?? throw new InvalidOperationException("Current User is not present");
        var vehicle = await vehiclesRepository.GetById(currentUser.AgentId, request.Id) ?? throw new NotFoundException("Pojazd o podanym numerze rejestracyjnym nie istnieje");
        
        await vehiclesRepository.Delete(vehicle);
    }
}
