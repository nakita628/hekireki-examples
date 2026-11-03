#nullable enable

using System;

namespace Langfuse.Models;

public partial class Media
{
    public string Id { get; set; } = null!;

    public string Sha256Hash { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? UploadedAt { get; set; }

    public int? UploadHttpStatus { get; set; }

    public string? UploadHttpError { get; set; }

    public string BucketPath { get; set; } = null!;

    public string BucketName { get; set; } = null!;

    public string ContentType { get; set; } = null!;

    public long ContentLength { get; set; }
}
