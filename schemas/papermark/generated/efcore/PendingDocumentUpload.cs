#nullable enable

using System;

namespace Papermark.Models;

public partial class PendingDocumentUpload
{
    public string Id { get; set; } = null!;

    public string UploadId { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public string TokenId { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string ContentType { get; set; } = null!;

    public DocumentStorageType StorageType { get; set; } = DocumentStorageType.S3Path;

    public DateTime ExpiresAt { get; set; }

    public DateTime? ConsumedAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
