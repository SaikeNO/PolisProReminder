using AutoMapper;
using MediatR;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Queries.GetAllInsurers;

public class GetAllInsurersCommandHandler(IInsurersRepository insurersRepository, IMapper mapper) : IRequestHandler<GetAllInsurersCommand, IEnumerable<InsurerDto>>
{
    public async Task<IEnumerable<InsurerDto>> Handle(GetAllInsurersCommand request, CancellationToken cancellationToken)
    {
        var insurers = await insurersRepository.GetAll();

        return mapper.Map<List<InsurerDto>>(insurers);
    }
}
