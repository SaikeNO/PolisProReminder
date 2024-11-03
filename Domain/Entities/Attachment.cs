﻿using PolisProReminder.Domain.Interfaces;

namespace PolisProReminder.Domain.Entities;

public class Attachment: ISoftDeletable, ICreatedBy
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FileName { get; set; } = null!;
    public string UniqueFileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    public Guid CreatedByUserId { get; set; }
    public Guid CreatedByAgentId { get; set; }
    public bool IsDeleted { get; set; } = false;

    public Attachment() { }

    public Attachment(string fileName, string savePath)
    {
        FileName = fileName;
        UniqueFileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Id}_{fileName}";
        FilePath = Path.Combine(savePath, UniqueFileName);
    }
}
