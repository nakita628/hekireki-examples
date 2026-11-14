#nullable enable

using System;

namespace Langfuse.Models;

public partial class BatchAction
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ActionType { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? FinishedAt { get; set; }

    public string Query { get; set; } = null!;

    public string? Config { get; set; }

    public int? TotalCount { get; set; }

    public int? ProcessedCount { get; set; }

    public int? FailedCount { get; set; }

    public string? Log { get; set; }

    public string? Cursor { get; set; }
}
