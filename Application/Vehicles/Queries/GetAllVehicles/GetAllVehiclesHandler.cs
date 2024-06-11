using AutoMapper;
using MediatR;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Users;
using PolisProReminder.Application.Vehicles.Dtos;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Vehicles.Queries.GetAllVehicles;

public class GetAllVehiclesHandler(IUserContext userContext,
    IVehiclesRepository vehiclesRepository,
    IMapper mapper) : IRequestHandler<GetAllVehiclesQuery, PageResult<VehicleDto>>
{
    public async Task<PageResult<VehicleDto>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var (vehicles, totalCount) = await vehiclesRepository.GetAllMatchingAsync(currentUser.AgentId,
            request.SearchPhrase,
            request.PageSize,
            request.PageIndex,
            request.SortBy,
            request.SortDirection);

        var vehiclesDtos = mapper.Map<List<VehicleDto>>(vehicles);

        var result = new PageResult<VehicleDto>(vehiclesDtos, totalCount, request.PageSize, request.PageIndex);
        return result;
    }
}
