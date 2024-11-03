using MediatR;
using PolisProReminder.Application.Users;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Attachments.Commands.UploadAttachment;

internal class UploadAttachmentCommandHandler(IUserContext userContext, IAttachmentsRepository attachmentsRepository) : IRequestHandler<UploadAttachmentCommand, Guid>
{
    public async Task<Guid> Handle(UploadAttachmentCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        _ = currentUser ?? throw new InvalidOperationException("Current User is not present");

        var savePath = Path.Combine(currentUser.AgentId.ToString());

        var attachment = new Attachment(request.Attachment.FileName, savePath)
        {
            CreatedByAgentId = currentUser.AgentId,
            CreatedByUserId = currentUser.Id
        };

        await attachmentsRepository.UploadAndSaveAttachmentAsync(attachment, request.Attachment);

        return attachment.Id;
    }
}
