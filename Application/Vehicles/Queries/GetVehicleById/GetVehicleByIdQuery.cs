using MediatR;
using PolisProReminder.Application.Vehicles.Dtos;

namespace PolisProReminder.Application.Vehicles.Queries.GetVehicleById;

public class GetVehicleByIdQuery(Guid id) : IRequest<VehicleDto>
{
    public Guid Id { get; } = id;
}
