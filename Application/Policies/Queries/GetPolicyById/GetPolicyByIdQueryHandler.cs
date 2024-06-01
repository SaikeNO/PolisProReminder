using AutoMapper;
using MediatR;
using PolisProReminder.Application.Policies.Dtos;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Policies.Queries.GetPolicyById;

public class GetPolicyByIdQueryHandler(IPoliciesRepository policiesRepository, IMapper mapper) : IRequestHandler<GetPolicyByIdQuery, PolicyDto>
{
    public async Task<PolicyDto> Handle(GetPolicyByIdQuery request, CancellationToken cancellationToken)
    {
        var policy = await policiesRepository.GetById(request.Id);

        _ = policy ?? throw new NotFoundException("Polisa o podanym ID nie istnieje");

        return mapper.Map<PolicyDto>(policy);
    }
}
