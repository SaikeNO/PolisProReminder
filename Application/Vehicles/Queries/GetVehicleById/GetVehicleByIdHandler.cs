using AutoMapper;
using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Application.Vehicles.Dtos;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Vehicles.Queries.GetVehicleById;

public class GetVehicleByIdHandler(IUserContext userContext,
    IVehiclesRepository vehiclesRepository,
    IMapper mapper) : IRequestHandler<GetVehicleByIdQuery, VehicleDto>
{
    public async Task<VehicleDto> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var vehicle = await vehiclesRepository.GetById(currentUser.AgentId, request.Id);

        _ = vehicle ?? throw new NotFoundException("Pojazd o podanym ID nie istnieje");

        return mapper.Map<VehicleDto>(vehicle);
    }
}
