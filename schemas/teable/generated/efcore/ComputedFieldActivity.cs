#nullable enable

using System;

namespace Teable.Models;

public partial class ComputedFieldActivity
{
    public string FieldId { get; set; } = null!;

    public string TableId { get; set; } = null!;

    public string BaseId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int ActiveTaskCount { get; set; } = 0;

    public int ProcessingTaskCount { get; set; } = 0;

    public long Generation { get; set; } = 0L;

    public long EstimatedComplexity { get; set; } = 0L;

    public long EstimatedDirtyRecords { get; set; } = 0L;

    public bool HasAllTargetRecords { get; set; } = false;

    public DateTime? QueuedAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? LastCompletedAt { get; set; }

    public int? LastDurationMs { get; set; }

    public string? LastError { get; set; }

    public string? Extensions { get; set; }

    public DateTime UpdatedAt { get; set; }
}
