using AutoMapper;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Attachments.Dtos;

public class AttachmentsProfile : Profile
{
    public AttachmentsProfile()
    {
        CreateMap<Attachment, AttachmentDto>()
            .ReverseMap();
    }
}
