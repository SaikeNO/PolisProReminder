using MediatR;

namespace PolisProReminder.Application.Attachments.Queries.GetAttachment;

public class GetAttachmentQuery(Guid attachmentId) : IRequest<(FileStream, string)?>
{
    public Guid AttachmentId { get; } = attachmentId;
}
