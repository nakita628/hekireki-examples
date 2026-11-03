#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class TaskRunAttempt
{
    public string Id { get; set; } = null!;

    public int Number { get; set; } = 0;

    public string FriendlyId { get; set; } = null!;

    public virtual TaskRun TaskRun { get; set; } = null!;

    public string TaskRunId { get; set; } = null!;

    public virtual BackgroundWorker BackgroundWorker { get; set; } = null!;

    public string BackgroundWorkerId { get; set; } = null!;

    public virtual BackgroundWorkerTask BackgroundWorkerTask { get; set; } = null!;

    public string BackgroundWorkerTaskId { get; set; } = null!;

    public virtual RuntimeEnvironment RuntimeEnvironment { get; set; } = null!;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public virtual TaskQueue Queue { get; set; } = null!;

    public string QueueId { get; set; } = null!;

    public TaskRunAttemptStatus Status { get; set; } = TaskRunAttemptStatus.Pending;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public int UsageDurationMs { get; set; } = 0;

    public string? Error { get; set; }

    public string? Output { get; set; }

    public string OutputType { get; set; } = "application/json";

    public virtual ICollection<TaskRunDependency> Dependencies { get; set; } = new List<TaskRunDependency>();

    public virtual ICollection<BatchTaskRun> BatchDependencies { get; set; } = new List<BatchTaskRun>();

    public virtual ICollection<Checkpoint> Checkpoints { get; set; } = new List<Checkpoint>();

    public virtual ICollection<BatchTaskRunItem> BatchTaskRunItems { get; set; } = new List<BatchTaskRunItem>();

    public virtual ICollection<CheckpointRestoreEvent> CheckpointRestoreEvent { get; set; } = new List<CheckpointRestoreEvent>();

    public virtual ICollection<TaskRun> ChildRuns { get; set; } = new List<TaskRun>();
}
