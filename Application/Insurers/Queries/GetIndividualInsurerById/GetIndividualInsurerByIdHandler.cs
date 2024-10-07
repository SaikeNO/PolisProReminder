using AutoMapper;
using MediatR;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Queries.GetIndividualInsurerById;

public class GetIndividualInsurerByIdHandler(IIndividualInsurersRepository insurersRepository,
    IMapper mapper,
    IUserContext userContext) : IRequestHandler<GetIndividualInsurerByIdQuery, IndividualInsurerDto>
{
    public async Task<IndividualInsurerDto> Handle(GetIndividualInsurerByIdQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var insurer = await insurersRepository.GetById(currentUser.AgentId, request.Id);

        return mapper.Map<IndividualInsurerDto>(insurer);
    }
}
