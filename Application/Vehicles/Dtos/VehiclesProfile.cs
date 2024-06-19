using AutoMapper;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Vehicles.Dtos;

public class VehiclesProfile : Profile
{
    public VehiclesProfile()
    {
        CreateMap<Vehicle, VehicleDto>().ForMember(v => v.VehicleBrand, opt => opt.MapFrom(v => v.VehicleBrand.Name));
        CreateMap<Insurer, VehicleInsurerDto>();
    }
}
