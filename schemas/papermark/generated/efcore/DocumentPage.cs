#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class DocumentPage
{
    public string Id { get; set; } = null!;

    public virtual DocumentVersion Version { get; set; } = null!;

    public string VersionId { get; set; } = null!;

    public int PageNumber { get; set; }

    public List<string>? EmbeddedLinks { get; set; }

    public string? PageLinks { get; set; }

    public string? Metadata { get; set; }

    public string File { get; set; } = null!;

    public DocumentStorageType StorageType { get; set; } = DocumentStorageType.VercelBlob;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
