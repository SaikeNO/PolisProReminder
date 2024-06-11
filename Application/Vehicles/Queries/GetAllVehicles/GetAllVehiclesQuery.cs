using MediatR;
using PolisProReminder.Application.Common;
using PolisProReminder.Application.Vehicles.Dtos;
using PolisProReminder.Domain.Constants;

namespace PolisProReminder.Application.Vehicles.Queries.GetAllVehicles;

public class GetAllVehiclesQuery : IRequest<PageResult<VehicleDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
