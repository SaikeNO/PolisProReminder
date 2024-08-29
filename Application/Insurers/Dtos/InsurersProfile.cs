using AutoMapper;
using PolisProReminder.Application.Insurers.Commands.CreateInsurer;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Insurers.Dtos;

public class InsurersProfile : Profile
{
    public InsurersProfile()
    {
        CreateMap<CreateInsurerCommand, Insurer>();

        CreateMap<Insurer, PolicyInsurerDto>()
            .ReverseMap();

        CreateMap<Insurer, InsurerDto>();
    }

}
