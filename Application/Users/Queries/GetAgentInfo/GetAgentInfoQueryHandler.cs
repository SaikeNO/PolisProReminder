using MediatR;
using Microsoft.AspNetCore.Identity;
using PolisProReminder.Application.Users.Dtos;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Users.Queries.GetAgentInfo;

internal sealed class GetAgentInfoQueryHandler(IUserContext userContext, IUsersRepository usersRepository, UserManager<User> userManager) : IRequestHandler<GetAgentInfoQuery, BaseUserDto>
{
    private readonly IUserContext _userContext = userContext;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<BaseUserDto> Handle(GetAgentInfoQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser() ?? throw new InvalidOperationException("Current User is not present");

        var agent = await _usersRepository.GetUserAsync(currentUser.AgentId, cancellationToken)
            ?? throw new InvalidOperationException("Agent not found");

        var agentDto = new BaseUserDto
        {
            FirstName = agent.FirstName,
            LastName = agent.LastName,
            Email = await _userManager.GetEmailAsync(agent) ?? throw new NotSupportedException("Users must have an email."),
        };

        return agentDto;
    }
}
