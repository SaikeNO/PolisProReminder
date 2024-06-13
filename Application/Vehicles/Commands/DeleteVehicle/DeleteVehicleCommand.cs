using MediatR;

namespace PolisProReminder.Application.Vehicles.Commands.DeleteVehicle;

public class DeleteVehicleCommand(Guid id) : IRequest
{
    public Guid Id { get; } = id;
}
