using MediatR;
using Microsoft.AspNetCore.Identity;
using PolisProReminder.Application.Users.Dtos;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Users.Queries.Info;

internal sealed class GetInfoQueryHandler(UserManager<User> userManager, IUserContext userContext, IUsersRepository usersRepository) : IRequestHandler<GetInfoQuery, UserDto>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IUserContext _userContext = userContext;
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task<UserDto> Handle(GetInfoQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser() ?? throw new InvalidOperationException("Current User is not present");

        var user = await _usersRepository.GetUserAsync(currentUser.Id, cancellationToken)
            ?? throw new InvalidOperationException("User not found");

        var userDto = new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = await _userManager.GetEmailAsync(user) ?? throw new NotSupportedException("Users must have an email."),
            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user),
            Roles = await _userManager.GetRolesAsync(user),
        };

        return userDto;
    }
}
