#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class TaskRunExecutionSnapshot
{
    public string Id { get; set; } = null!;

    public RunEngineVersion Engine { get; set; } = RunEngineVersion.V2;

    public TaskRunExecutionStatus ExecutionStatus { get; set; }

    public string Description { get; set; } = null!;

    public bool IsValid { get; set; } = true;

    public string? Error { get; set; }

    public string? PreviousSnapshotId { get; set; }

    public string RunId { get; set; } = null!;

    public virtual TaskRun Run { get; set; } = null!;

    public TaskRunStatus RunStatus { get; set; }

    public string? BatchId { get; set; }

    public int? AttemptNumber { get; set; }

    public string EnvironmentId { get; set; } = null!;

    public RuntimeEnvironmentType EnvironmentType { get; set; }

    public string ProjectId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual ICollection<Waitpoint> CompletedWaitpoints { get; set; } = new List<Waitpoint>();

    public List<string>? CompletedWaitpointOrder { get; set; }

    public string? CheckpointId { get; set; }

    public virtual TaskRunCheckpoint? Checkpoint { get; set; }

    public string? WorkerId { get; set; }

    public string? RunnerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? LastHeartbeatAt { get; set; }

    public string? Metadata { get; set; }
}
