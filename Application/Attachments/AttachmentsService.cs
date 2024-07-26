using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PolisProReminder.Application.Attachments.Dtos;
using PolisProReminder.Domain.Exceptions;

namespace PolisProReminder.Application.Attachments;

internal class AttachmentsService(IConfiguration configuration) : IAttachmentsService
{
    public async Task<IEnumerable<AttachmentDto>> UploadAttachmentsAsync(IEnumerable<IFormFile> files, string savePath)
    {
        List<AttachmentDto> attachments = [];
        var storagePath = configuration["StoragePath"];
        if (storagePath == null)
            throw new ArgumentNullException(nameof(storagePath));

        foreach (var attachment in files)
        {
            AttachmentDto a = new AttachmentDto()
            {
                FileName = attachment.FileName,
            };

            var uniqueFileName = $"{attachment.FileName}_{a.Id}.{Path.GetExtension(attachment.FileName)}";
            var filePath = Path.Combine(savePath, uniqueFileName);
            var fullFilePath = Path.Combine(storagePath, filePath);
            Directory.CreateDirectory(Path.GetDirectoryName(fullFilePath));
            try
            {
                using (var stream = File.Create(fullFilePath))
                {
                    await attachment.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                throw new UploadAttachmentException(filePath);
            }

            a.UniqueFileName = uniqueFileName;
            a.FilePath = filePath;
            attachments.Add(a);
        }

        return attachments;
    }
}
