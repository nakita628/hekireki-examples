#nullable enable

using System;

namespace Langfuse.Models;

public partial class BatchExport
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime? FinishedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public string Name { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Query { get; set; } = null!;

    public string Format { get; set; } = null!;

    public string? Url { get; set; }

    public string? Log { get; set; }
}
