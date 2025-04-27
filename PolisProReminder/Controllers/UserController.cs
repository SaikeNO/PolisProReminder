using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Application.Users.Commands.AssignUserRole;
using PolisProReminder.Application.Users.Commands.ChangePassword;
using PolisProReminder.Application.Users.Commands.UnassignUserRole;
using PolisProReminder.Application.Users.Commands.UpdateUserDetails;
using PolisProReminder.Application.Users.Dtos;
using PolisProReminder.Application.Users.Queries.GetAssistants;
using PolisProReminder.Domain.Constants;

namespace PolisProReminder.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPatch("user")]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("userRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("userRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("changePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpGet("assistants")]
    [Authorize(Roles = UserRoles.Agent)]
    public async Task<ActionResult<IEnumerable<AssistantDto>>> GetAssistants(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAssistantsQuery(), cancellationToken);

        return Ok(result);
    }
}
