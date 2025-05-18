using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Application.Users.Commands.AssignUserRole;
using PolisProReminder.Application.Users.Commands.CreateAssistant;
using PolisProReminder.Application.Users.Commands.DeleteAssistant;
using PolisProReminder.Application.Users.Commands.LockoutAssistant;
using PolisProReminder.Application.Users.Commands.UnassignUserRole;
using PolisProReminder.Application.Users.Commands.UnlockAssistant;
using PolisProReminder.Application.Users.Commands.UpdateUserDetails;
using PolisProReminder.Application.Users.Dtos;
using PolisProReminder.Application.Users.Queries.GetAssistants;
using PolisProReminder.Application.Users.Queries.Info;
using PolisProReminder.Domain.Constants;

namespace PolisProReminder.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPatch("info")]
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

    [HttpPost("assistant")]
    [Authorize(Roles = UserRoles.Agent)]
    public async Task<ActionResult<Guid>> CreateAssistant([FromBody] CreateAssistantCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpGet("assistant")]
    [Authorize(Roles = UserRoles.Agent)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAssistants(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAssistantsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpDelete("assistant/{assistantId:guid}")]
    [Authorize(Roles = UserRoles.Agent)]
    public async Task<IActionResult> DeleteAssistant([FromRoute] Guid assistantId, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteAssistantCommand { AssistantId = assistantId }, cancellationToken);

        return NoContent();
    }

    [HttpPatch("assistant/{assistantId:guid}/lockout")]
    [Authorize(Roles = UserRoles.Agent)]
    public async Task<IActionResult> LockoutAssistant([FromRoute] Guid assistantId, CancellationToken cancellationToken)
    {
        await mediator.Send(new LockoutAssistantCommand { AssistantId = assistantId }, cancellationToken);

        return NoContent();
    }

    [HttpPatch("assistant/{assistantId:guid}/unlock")]
    [Authorize(Roles = UserRoles.Agent)]
    public async Task<IActionResult> UnlockAssistant([FromRoute] Guid assistantId, CancellationToken cancellationToken)
    {
        await mediator.Send(new UnlockAssistantCommand { AssistantId = assistantId }, cancellationToken);

        return NoContent();
    }

    [HttpGet("info")]
    public async Task<ActionResult<UserDto>> GetInfo(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetInfoQuery(), cancellationToken);

        return Ok(result);
    }

}
