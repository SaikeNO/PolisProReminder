using AutoMapper;
using MediatR;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Queries.GetAllBusinessInsurers;

internal class GetAllBusinessInsurersHandler(IBusinessInsurersRepository insurersRepository,
    IMapper mapper,
    IUserContext userContext)
    : IRequestHandler<GetAllBusinessInsurersQuery, IEnumerable<BusinessInsurerDto>>
{
    public async Task<IEnumerable<BusinessInsurerDto>> Handle(GetAllBusinessInsurersQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var insurers = await insurersRepository.GetAllBusiness(currentUser.AgentId);

        return mapper.Map<IEnumerable<BusinessInsurerDto>>(insurers);
    }
}