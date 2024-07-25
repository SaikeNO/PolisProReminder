using AutoMapper;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Vehicles.Dtos;

public class VehiclesProfile : Profile
{
    public VehiclesProfile()
    {
        CreateMap<Vehicle, VehicleDto>();
        CreateMap<Insurer, VehicleInsurerDto>();
    }
}
