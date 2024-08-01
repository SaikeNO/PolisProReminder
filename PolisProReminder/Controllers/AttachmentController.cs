using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Application.Attachments.Commands.DeleteAttachment;
using PolisProReminder.Application.Attachments.Queries.GetAttachment;

namespace PolisProReminder.API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class AttachmentController(IMediator mediator) : ControllerBase
{
    [HttpGet("{attachmentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAttachment([FromRoute] Guid attachmentId)
    {
        var attachment = await mediator.Send(new GetAttachmentQuery(attachmentId));
        return File(attachment.Value.Item1, "application/octet-stream", attachment.Value.Item2);
    }

    [HttpDelete("{attachmentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAttachment([FromRoute] Guid attachmentId)
    {
        await mediator.Send(new DeleteAttachmentCommand(attachmentId));
        return NoContent();
    }
}
