using AutoMapper;
using PolisProReminder.Domain.Entities;

namespace PolisProReminder.Application.Attachments.Dtos;

public class AttachmentProfile : Profile
{
    public AttachmentProfile()
    {
        CreateMap<Attachment, AttachmentDto>();
    }
}
