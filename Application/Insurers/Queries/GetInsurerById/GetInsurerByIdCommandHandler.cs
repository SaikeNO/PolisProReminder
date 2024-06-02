using AutoMapper;
using MediatR;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Queries.GetInsurerById;

public class GetInsurerByIdCommandHandler(IInsurersRepository insurersRepository, IMapper mapper) : IRequestHandler<GetInsurerByIdCommand, InsurerDto>
{
    public async Task<InsurerDto> Handle(GetInsurerByIdCommand request, CancellationToken cancellationToken)
    {
        var insurer = await insurersRepository.GetById(request.Id);

        return mapper.Map<InsurerDto>(insurer);
    }
}
