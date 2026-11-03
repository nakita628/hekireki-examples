#nullable enable

using System;

namespace Trigger.Models;

public partial class TaskRunDependency
{
    public string Id { get; set; } = null!;

    public virtual TaskRun TaskRun { get; set; } = null!;

    public string TaskRunId { get; set; } = null!;

    public virtual CheckpointRestoreEvent? CheckpointEvent { get; set; }

    public string? CheckpointEventId { get; set; }

    public virtual TaskRunAttempt? DependentAttempt { get; set; }

    public string? DependentAttemptId { get; set; }

    public virtual BatchTaskRun? DependentBatchRun { get; set; }

    public string? DependentBatchRunId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? ResumedAt { get; set; }
}
