#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class Waitpoint
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public WaitpointType Type { get; set; }

    public WaitpointStatus Status { get; set; } = WaitpointStatus.Pending;

    public DateTime? CompletedAt { get; set; }

    public string IdempotencyKey { get; set; } = null!;

    public bool UserProvidedIdempotencyKey { get; set; }

    public DateTime? IdempotencyKeyExpiresAt { get; set; }

    public string? InactiveIdempotencyKey { get; set; }

    public string? CompletedByTaskRunId { get; set; }

    public virtual TaskRun? CompletedByTaskRun { get; set; }

    public DateTime? CompletedAfter { get; set; }

    public string? CompletedByBatchId { get; set; }

    public virtual BatchTaskRun? CompletedByBatch { get; set; }

    public virtual ICollection<TaskRunWaitpoint> BlockingTaskRuns { get; set; } = new List<TaskRunWaitpoint>();

    public virtual ICollection<TaskRun> ConnectedRuns { get; set; } = new List<TaskRun>();

    public virtual ICollection<TaskRunExecutionSnapshot> CompletedExecutionSnapshots { get; set; } = new List<TaskRunExecutionSnapshot>();

    public string? Output { get; set; }

    public string OutputType { get; set; } = "application/json";

    public bool OutputIsError { get; set; } = false;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual RuntimeEnvironment Environment { get; set; } = null!;

    public string EnvironmentId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<string>? Tags { get; set; }
}
