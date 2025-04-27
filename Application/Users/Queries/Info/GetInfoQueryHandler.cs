using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Application.Users.Dtos;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Users.Queries.Info;

internal class GetInfoQueryHandler(UserManager<User> userManager, IUserContext userContext) : IRequestHandler<GetInfoQuery, UserDto>
{
    public async Task<UserDto> Handle(GetInfoQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser() ?? throw new InvalidOperationException("Current User is not present");

        var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == currentUser.Id, cancellationToken)
            ?? throw new InvalidOperationException("User not found");

        var userDto = new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Roles = await userManager.GetRolesAsync(user).ContinueWith(t => t.Result.ToList(), cancellationToken)
        };

        return userDto;
    }
}

