#nullable enable

using System;

namespace Trigger.Models;

public partial class CheckpointRestoreEvent
{
    public string Id { get; set; } = null!;

    public CheckpointRestoreEventType Type { get; set; }

    public string? Reason { get; set; }

    public string? Metadata { get; set; }

    public virtual Checkpoint Checkpoint { get; set; } = null!;

    public string CheckpointId { get; set; } = null!;

    public virtual TaskRun Run { get; set; } = null!;

    public string RunId { get; set; } = null!;

    public virtual TaskRunAttempt Attempt { get; set; } = null!;

    public string AttemptId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual RuntimeEnvironment RuntimeEnvironment { get; set; } = null!;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public virtual TaskRunDependency? TaskRunDependency { get; set; }

    public virtual BatchTaskRun? BatchTaskRunDependency { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
