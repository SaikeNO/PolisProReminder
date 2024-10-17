using MediatR;
using PolisProReminder.Application.Vehicles.Commands.BaseVehicle;

namespace PolisProReminder.Application.Vehicles.Commands.UpdateVehicle;

public class UpdateVehicleCommand : BaseVehicleCommand, IRequest
{
    public Guid Id { get; set; }
}
