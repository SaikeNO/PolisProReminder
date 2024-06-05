using AutoMapper;
using MediatR;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Queries.GetAllInsurers;

public class GetAllInsurersHandler(IInsurersRepository insurersRepository, 
    IMapper mapper,
    IUserContext userContext) 
    : IRequestHandler<GetAllInsurersQuery, IEnumerable<InsurerDto>>
{
    public async Task<IEnumerable<InsurerDto>> Handle(GetAllInsurersQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var insurers = await insurersRepository.GetAll(currentUser.AgentId);

        return mapper.Map<List<InsurerDto>>(insurers);
    }
}
