using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Application.Users.Dtos;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Users.Queries.GetAssistants;

internal class GetAssistantsQueryHandler(UserManager<User> userManager, IUserContext userContext) : IRequestHandler<GetAssistantsQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetAssistantsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser() ?? throw new InvalidOperationException("Current User is not present");

        var assistants = await userManager.Users
            .Where(x => x.AgentId == currentUser.Id && x.Id != currentUser.Id)
            .Select(x => new UserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
            }).ToListAsync(cancellationToken);

        return assistants;
    }
}
