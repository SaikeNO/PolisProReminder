using AutoMapper;
using MediatR;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Queries.GetAllIndividualInsurers;

public class GetAllIndividualInsurersHandler(IIndividualInsurersRepository insurersRepository, 
    IMapper mapper,
    IUserContext userContext) 
    : IRequestHandler<GetAllIndividualInsurersQuery, IEnumerable<IndividualInsurerDto>>
{
    public async Task<IEnumerable<IndividualInsurerDto>> Handle(GetAllIndividualInsurersQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var insurers = await insurersRepository.GetAll(currentUser.AgentId);

        return mapper.Map<IEnumerable<IndividualInsurerDto>>(insurers);
    }
}
