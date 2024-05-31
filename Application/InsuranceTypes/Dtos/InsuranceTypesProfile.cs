using AutoMapper;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.InsuranceTypes.Dtos;

public class InsuranceTypesProfile : Profile
{
    public InsuranceTypesProfile()
    {
        CreateMap<InsuranceType, InsuranceTypeDto>()
            .ReverseMap();

        CreateMap<CreateInsuranceTypeDto, InsuranceType>();
        CreateMap<InsuranceTypeDto, CreateInsuranceTypeDto>();
    }
}
