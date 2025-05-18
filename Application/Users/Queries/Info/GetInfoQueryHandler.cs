using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Application.Users.Dtos;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Users.Queries.Info;

internal sealed class GetInfoQueryHandler(UserManager<User> userManager, IUserContext userContext) : IRequestHandler<GetInfoQuery, UserDto>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IUserContext _userContext = userContext;

    public async Task<UserDto> Handle(GetInfoQuery request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser() ?? throw new InvalidOperationException("Current User is not present");

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == currentUser.Id, cancellationToken)
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
