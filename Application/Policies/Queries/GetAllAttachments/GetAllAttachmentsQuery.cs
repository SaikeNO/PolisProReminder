using MediatR;
using PolisProReminder.Application.Attachments.Dtos;

namespace PolisProReminder.Application.Policies.Queries.GetAllAttachments;

public class GetAllAttachmentsQuery(Guid id) : IRequest<IEnumerable<AttachmentDto>>
{
    public Guid Id { get; } = id;
}
