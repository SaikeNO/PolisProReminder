using AutoMapper;
using MediatR;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Queries.GetAllInsurers;

public class GetAllInsurersCommandHandler(IInsurersRepository insurersRepository, 
    IMapper mapper,
    IUserContext userContext) 
    : IRequestHandler<GetAllInsurersCommand, IEnumerable<InsurerDto>>
{
    public async Task<IEnumerable<InsurerDto>> Handle(GetAllInsurersCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var insurers = await insurersRepository.GetAll(currentUser.AgentId);

        return mapper.Map<List<InsurerDto>>(insurers);
    }
}
