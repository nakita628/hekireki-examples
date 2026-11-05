#nullable enable

using System;

namespace InboxZero.Models;

public partial class AttachmentDocument
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string AttachmentSourceId { get; set; } = null!;

    public virtual AttachmentSource AttachmentSource { get; set; } = null!;

    public string FileId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string MimeType { get; set; } = null!;

    public DateTime? ModifiedAt { get; set; }

    public string? Summary { get; set; }

    public string? Content { get; set; }

    public string? Metadata { get; set; }

    public DateTime? IndexedAt { get; set; }

    public string? Error { get; set; }
}
