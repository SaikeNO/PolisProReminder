using AutoMapper;
using MediatR;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies.Queries.GetPolicyById;

public class GetPolicyByIdHandler(IUserContext userContext, 
    IPoliciesRepository policiesRepository, 
    IMapper mapper) : IRequestHandler<GetPolicyByIdQuery, PolicyDto>
{
    public async Task<PolicyDto> Handle(GetPolicyByIdQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var policy = await policiesRepository.GetById(currentUser.AgentId, request.Id);

        _ = policy ?? throw new NotFoundException("Polisa o podanym ID nie istnieje");

        return mapper.Map<PolicyDto>(policy);
    }
}
