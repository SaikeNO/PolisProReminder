using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;

namespace PolisProReminder.Infrastructure.Repositories;

internal class AttachmentsRepository(IConfiguration configuration, InsuranceDbContext dbContext) : IAttachmentsRepository
{
    private readonly string storagePath = configuration["StoragePath"] ?? throw new ArgumentNullException(nameof(storagePath));
    private readonly string storagePathDeleted = configuration["StoragePathDeleted"] ?? throw new ArgumentNullException(nameof(storagePathDeleted));

    public async Task<IEnumerable<Attachment>?> GetAll<TEntity>(Guid id) where TEntity : AttachmentList
    {
        var set = await dbContext.Set<TEntity>()
            .AsNoTracking()
            .Include(x => x.Attachments)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (set == null)
            return null;

        return set.Attachments.Where(a => a.IsDeleted == false);
    }

    public async Task<Attachment?> GetById(Guid id)
    {
        var attachment = await dbContext
            .Attachments
            .FirstOrDefaultAsync(x => x.Id == id);

        return attachment;
    }

    public void Delete(Attachment entity)
    {
        entity.IsDeleted = true;
        var fullFilePath = Path.Combine(storagePath, entity.FilePath);
        var fullFilePathToMove = Path.Combine(storagePathDeleted, entity.FilePath);

        Directory.CreateDirectory(Path.GetDirectoryName(fullFilePathToMove)!);
        File.Move(fullFilePath, fullFilePathToMove);
    }

    public async Task<IEnumerable<Attachment>> CreateAttachmentRangeAsync(IEnumerable<Attachment> attachments)
    {
        await dbContext.Attachments.AddRangeAsync(attachments);
        return attachments;
    }

    public async Task UploadAttachmentsAsync(IEnumerable<AttachmentFormFile> attachments)
    {
        foreach (var attachment in attachments)
        {
            var fullFilePath = Path.Combine(storagePath, attachment.FilePath);
            Directory.CreateDirectory(Path.GetDirectoryName(fullFilePath)!);

            using (var stream = File.Create(fullFilePath))
            {
                await attachment.File.CopyToAsync(stream);
            }
        }
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
