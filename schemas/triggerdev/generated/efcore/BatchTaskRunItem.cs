#nullable enable

using System;

namespace Trigger.Models;

public partial class BatchTaskRunItem
{
    public string Id { get; set; } = null!;

    public BatchTaskRunItemStatus Status { get; set; } = BatchTaskRunItemStatus.Pending;

    public virtual BatchTaskRun BatchTaskRun { get; set; } = null!;

    public string BatchTaskRunId { get; set; } = null!;

    public virtual TaskRun TaskRun { get; set; } = null!;

    public string TaskRunId { get; set; } = null!;

    public virtual TaskRunAttempt? TaskRunAttempt { get; set; }

    public string? TaskRunAttemptId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }
}
