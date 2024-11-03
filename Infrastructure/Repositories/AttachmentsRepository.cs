using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PolisProReminder.Domain.Entities;
using PolisProReminder.Domain.Exceptions;
using PolisProReminder.Domain.Repositories;
using PolisProReminder.Infrastructure.Persistance;
using PolisProReminder.Infrastructure.Settings;

namespace PolisProReminder.Infrastructure.Repositories;

internal class AttachmentsRepository(AttachmentsSettings attachmentsSettings, InsuranceDbContext dbContext) : IAttachmentsRepository
{
    public async Task<Attachment?> GetById(Guid id)
    {
        var attachment = await dbContext
            .Attachments
            .FirstOrDefaultAsync(x => x.Id == id);

        return attachment;
    }

    public async Task<IEnumerable<Attachment>> GetManyByIds(IEnumerable<Guid> ids)
    {
        var attachments = await dbContext
            .Attachments
            .Where(a => ids.Contains(a.Id))
            .ToListAsync();

        return attachments;
    }

    public void Delete(Attachment entity)
    {
        entity.IsDeleted = true;
        var fullFilePath = Path.Combine(attachmentsSettings.StoragePath, entity.FilePath);
        var fullFilePathToMove = Path.Combine(attachmentsSettings.StoragePathDeleted, entity.FilePath);

        Directory.CreateDirectory(Path.GetDirectoryName(fullFilePathToMove)!);
        File.Move(fullFilePath, fullFilePathToMove);
    }

    public async Task SaveAttachmentAsync(Attachment attachment)
    {
        await dbContext.Attachments.AddAsync(attachment);
    }

    public async Task UploadAttachmentAsync(IFormFile attachment, string savePath)
    {
        var fullFilePath = Path.Combine(attachmentsSettings.StoragePath, savePath);
        Directory.CreateDirectory(Path.GetDirectoryName(fullFilePath)!);

        using (var stream = File.Create(fullFilePath))
        {
            await attachment.CopyToAsync(stream);
        }
    }

    public async Task UploadAndSaveAttachmentAsync(Attachment attachment, IFormFile file)
    {
        using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            await UploadAttachmentAsync(file, attachment.FilePath);
            await SaveAttachmentAsync(attachment);

            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            RemoveAttachment(attachment.FileName);

            throw new FileUploadException("Nie można zapisać pliku.");
        }
    }

    public async Task<(FileStream, string)?> GetAttachmentAsync(Guid attachmentId)
    {
        var attachment = await GetById(attachmentId);
        if (attachment == null)
            return null;

        var filePath = Path.Combine(attachmentsSettings.StoragePath, attachment.FilePath);

        if (File.Exists(filePath))
        {
            return (File.OpenRead(filePath), attachment.FileName);
        }

        return null;
    }

    public Task SaveChanges() => dbContext.SaveChangesAsync();

    private void RemoveAttachment(string filePath)
    {
        var fullFilePath = Path.Combine(attachmentsSettings.StoragePath, filePath);

        if (File.Exists(fullFilePath))
        {
            File.Delete(fullFilePath);
        }
    }
}
