using AutoMapper;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.VehicleBrands.Dtos;

public class VehicleBrandsProfile : Profile
{
    public VehicleBrandsProfile()
    {
        CreateMap<VehicleBrand, VehicleBrandDto>();
    }
}
