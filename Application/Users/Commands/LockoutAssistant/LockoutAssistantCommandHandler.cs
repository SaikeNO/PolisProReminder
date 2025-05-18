using MediatR;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Users.Commands.LockoutAssistant;

internal sealed class LockoutAssistantCommandHandler(IUserContext userContext, IUsersRepository usersRepository) : IRequestHandler<LockoutAssistantCommand>
{
    private readonly IUserContext _userContext = userContext;
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task Handle(LockoutAssistantCommand request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser()
            ?? throw new NotFoundException("User not found.");

        var assistant = await _usersRepository.GetUserAsync(request.AssistantId, cancellationToken)
            ?? throw new NotFoundException($"Assistant with ID {request.AssistantId} not found.");

        if (assistant.AgentId != user.Id)
        {
            throw new NotAllowedException("You cannot lockout user who is not your assistant");
        }

        if (assistant.LockoutEnd == DateTimeOffset.MaxValue)
        {
            throw new NotAllowedException("User is already locked.");
        }

        await _usersRepository.LockoutUserAsync(assistant, cancellationToken);
    }
}
