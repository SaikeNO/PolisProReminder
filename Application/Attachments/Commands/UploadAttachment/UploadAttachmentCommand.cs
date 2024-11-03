using MediatR;
using Microsoft.AspNetCore.Http;

namespace PolisProReminder.Application.Attachments.Commands.UploadAttachment;

public record UploadAttachmentCommand(IFormFile Attachment) : IRequest<Guid>
{
    public IFormFile Attachment { get; set; } = Attachment;
}
