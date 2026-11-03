#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class DocumentVersion
{
    public string Id { get; set; } = null!;

    public int VersionNumber { get; set; }

    public virtual Document Document { get; set; } = null!;

    public string DocumentId { get; set; } = null!;

    public string File { get; set; } = null!;

    public string? OriginalFile { get; set; }

    public string? Type { get; set; }

    public string? ContentType { get; set; }

    public long? FileSize { get; set; }

    public DocumentStorageType StorageType { get; set; } = DocumentStorageType.VercelBlob;

    public int? NumPages { get; set; }

    public bool IsPrimary { get; set; } = false;

    public bool IsVertical { get; set; } = false;

    public string? FileId { get; set; }

    public string? VectorStoreFileId { get; set; }

    public virtual ICollection<DocumentPage> Pages { get; set; } = new List<DocumentPage>();

    public virtual ICollection<DocumentRedactionJob> RedactionJobs { get; set; } = new List<DocumentRedactionJob>();

    public bool HasPages { get; set; } = false;

    public int? Length { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
