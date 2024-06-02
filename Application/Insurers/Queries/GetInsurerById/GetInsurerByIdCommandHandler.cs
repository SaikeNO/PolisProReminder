using AutoMapper;
using MediatR;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Queries.GetInsurerById;

public class GetInsurerByIdCommandHandler(IInsurersRepository insurersRepository, 
    IMapper mapper,
    IUserContext userContext) : IRequestHandler<GetInsurerByIdCommand, InsurerDto>
{
    public async Task<InsurerDto> Handle(GetInsurerByIdCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var insurer = await insurersRepository.GetById(currentUser.AgentId, request.Id);

        return mapper.Map<InsurerDto>(insurer);
    }
}
