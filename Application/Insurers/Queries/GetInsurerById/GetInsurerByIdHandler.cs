using AutoMapper;
using MediatR;
using PolisProReminder.Application.Insurers.Dtos;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Insurers.Queries.GetInsurerById;

public class GetInsurerByIdHandler(IBaseInsurersRepository insurersRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<GetInsurerByIdQuery, BaseInsurerDto>
{
    public async Task<BaseInsurerDto> Handle(GetInsurerByIdQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var insurer = await insurersRepository.GetById(currentUser.AgentId, request.Id);

        if (insurer is IndividualInsurer individualInsurer)
        {
            return mapper.Map<IndividualInsurerDto>(individualInsurer);
        }
        else if (insurer is BusinessInsurer businessInsurer)
        {
            return mapper.Map<BusinessInsurerDto>(businessInsurer);
        }

        throw new InvalidOperationException("Klient nie jest typu ani indywidualnego ani bizensowego");
    }
}
