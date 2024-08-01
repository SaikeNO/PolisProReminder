using MediatR;

namespace PolisProReminder.Application.Attachments.Commands.DeleteAttachment;

public class DeleteAttachmentCommand(Guid attachmentId) : IRequest
{
    public Guid AttachmentId { get; } = attachmentId;
}