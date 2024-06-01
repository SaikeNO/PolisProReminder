using AutoMapper;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Insurers.Dtos;

public class InsurersProfile : Profile
{
    public InsurersProfile()
    {
        CreateMap<Insurer, PolicyInsurerDto>()
            .ReverseMap();

        CreateMap<Insurer, InsurerDto>();

        CreateMap<CreateInsurerDto, Insurer>()
            .ReverseMap();
    }

}
