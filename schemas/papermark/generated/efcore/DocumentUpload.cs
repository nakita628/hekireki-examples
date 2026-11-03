#nullable enable

using System;

namespace Papermark.Models;

public partial class DocumentUpload
{
    public string Id { get; set; } = null!;

    public string DocumentId { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public string? ViewerId { get; set; }

    public virtual Viewer? Viewer { get; set; }

    public string? ViewId { get; set; }

    public virtual View? View { get; set; }

    public string LinkId { get; set; } = null!;

    public virtual Link Link { get; set; } = null!;

    public string? DataroomId { get; set; }

    public virtual Dataroom? Dataroom { get; set; }

    public string? DataroomDocumentId { get; set; }

    public virtual DataroomDocument? DataroomDocument { get; set; }

    public string? TaskId { get; set; }

    public virtual Task? Task { get; set; }

    public string? OriginalFilename { get; set; }

    public long? FileSize { get; set; }

    public int? NumPages { get; set; }

    public string? MimeType { get; set; }

    public DateTime UploadedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
