using AutoMapper;
using MediatR;
using PolisProReminder.Application.Attachments.Dtos;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;

namespace PolisProReminder.Application.Vehicles.Queries.GetAllAttachments;

internal class GetAllAtachmentsHandler(IAttachmentsRepository attachmentsRepository,
    IMapper mapper) : IRequestHandler<GetAllAttachmentsQuery, IEnumerable<AttachmentDto>>
{
    public async Task<IEnumerable<AttachmentDto>> Handle(GetAllAttachmentsQuery request, CancellationToken cancellationToken)
    {
        var attachments = await attachmentsRepository.GetAll<Vehicle>(request.Id);

        if (attachments == null)
            throw new NotFoundException("Pojazd o podanym ID nie istnieje");

        return mapper.Map<IEnumerable<AttachmentDto>>(attachments);
    }
}
