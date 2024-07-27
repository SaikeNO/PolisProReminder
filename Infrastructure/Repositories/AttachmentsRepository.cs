using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class AttachmentsRepository(IConfiguration configuration, InsuranceDbContext dbContext) : IAttachmentsRepository
{
    private readonly string storagePath = configuration["StoragePath"] ?? throw new ArgumentNullException(nameof(storagePath));

    public async Task<IEnumerable<Attachment>?> GetAll<TEntity>(Guid id) where TEntity : AttachmentList
    {
        var set = await dbContext.Set<TEntity>()
            .AsNoTracking()
            .Include(x => x.Attachments)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (set == null)
            return null;

        return set.Attachments;
    }

    public async Task<Attachment?> GetById(Guid id)
    {
        var company = await dbContext
            .Attachments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return company;
    }

    public async Task Delete(Attachment entity)
    {
        entity.IsDeleted = true;

        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Attachment>> UploadAttachmentsAsync(IEnumerable<IFormFile> files, string savePath)
    {
        List<Attachment> attachments = [];
        if (storagePath == null)
            throw new ArgumentNullException(nameof(storagePath));

        foreach (var attachment in files)
        {
            var a = new Attachment()
            {
                FileName = attachment.FileName,
            };

            var uniqueFileName = $"{DateTime.Now:yyyyMMddHHmmssffff}_{a.Id}_{attachment.FileName}";
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

        await dbContext.Attachments.AddRangeAsync(attachments);
        await SaveChanges();

        return attachments;
    }

    public async Task<(FileStream, string)?> GetAttachmentAsync(Guid attachmentId)
    {
        var attachment = await GetById(attachmentId);
        if (attachment == null)
            return null;

        var filePath = Path.Combine(storagePath, attachment.FilePath);

        if (File.Exists(filePath))
        {
            return (File.OpenRead(filePath), attachment.FileName);
        }

        return null;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();
}
