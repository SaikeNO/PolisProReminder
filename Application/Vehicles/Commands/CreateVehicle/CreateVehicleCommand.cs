using MediatR;
using PolisProReminder.Application.Vehicles.Commands.BaseVehicle;

namespace PolisProReminder.Application.Vehicles.Commands.CreateVehicle;

public class CreateVehicleCommand : BaseVehicleCommand, IRequest<Guid> { }