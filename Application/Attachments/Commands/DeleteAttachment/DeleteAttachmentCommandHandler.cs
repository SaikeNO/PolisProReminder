using MediatR;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Attachments.Commands.DeleteAttachment;

internal class DeleteAttachmentCommandHandler(IAttachmentsRepository attachmentsRepository) : IRequestHandler<DeleteAttachmentCommand>
{
    public async Task Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
    {
        var attachment = await attachmentsRepository.GetById(request.AttachmentId);
        _ = attachment ?? throw new NotFoundException("Plik o podanym ID nie istnieje");

        attachmentsRepository.Delete(attachment);
        await attachmentsRepository.SaveChanges();
    }
}
