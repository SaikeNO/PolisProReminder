using MediatR;
using Microsoft.AspNetCore.Identity;
using PolisProReminder.Application.Users.Dtos;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Users.Queries.GetAssistants;

internal sealed class GetAssistantsQueryHandler(UserManager<User> userManager, IUsersRepository usersRepository, IUserContext userContext) : IRequestHandler<GetAssistantsQuery, IEnumerable<UserDto>>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IUserContext _userContext = userContext;

    public async Task<IEnumerable<UserDto>> Handle(GetAssistantsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser() ?? throw new InvalidOperationException("Current User is not present");

        var assistants = await _usersRepository.GetAssistantsAsync(currentUser.Id, cancellationToken);

        var assistantDtos = new List<UserDto>();

        foreach (var assistant in assistants)
        {
            assistantDtos.Add(new UserDto
            {
                Id = assistant.Id,
                FirstName = assistant.FirstName,
                LastName = assistant.LastName,
                Email = await _userManager.GetEmailAsync(assistant) ?? throw new NotSupportedException("Users must have an email."),
                IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(assistant),
                IsLockedOut = await _userManager.IsLockedOutAsync(assistant),
                Roles = await _userManager.GetRolesAsync(assistant)
            });
        }

        return assistantDtos;
    }
}
