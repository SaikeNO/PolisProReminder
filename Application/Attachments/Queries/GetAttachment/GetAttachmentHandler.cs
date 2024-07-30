using MediatR;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Attachments.Queries.GetAttachment;

internal class GetAttachmentHandler(IAttachmentsRepository attachmentsRepository) : IRequestHandler<GetAttachmentQuery, (FileStream, string)?>
{

    public async Task<(FileStream, string)?> Handle(GetAttachmentQuery request, CancellationToken cancellationToken)
    {

        var attachment = await attachmentsRepository.GetAttachmentAsync(request.AttachmentId);

        _ = attachment ?? throw new NotFoundException("Plik o podanym ID nie istnieje");

        return (attachment.Value.Item1, attachment.Value.Item2);
    }
}
