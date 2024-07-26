using Microsoft.AspNetCore.Http;
using PolisProReminder.Application.Attachments.Dtos;

namespace PolisProReminder.Application.Attachments;

public interface IAttachmentsService
{
    Task<IEnumerable<AttachmentDto>> UploadAttachmentsAsync(IEnumerable<IFormFile> files, string savePath);
}