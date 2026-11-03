#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class BatchTaskRun
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public string? IdempotencyKey { get; set; }

    public DateTime? IdempotencyKeyExpiresAt { get; set; }

    public BatchTaskRunStatus Status { get; set; } = BatchTaskRunStatus.Pending;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public virtual ICollection<TaskRun> Runs { get; set; } = new List<TaskRun>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<string>? RunIds { get; set; } = new List<string>();

    public int RunCount { get; set; } = 0;

    public string? Payload { get; set; }

    public string PayloadType { get; set; } = "application/json";

    public string? Options { get; set; }

    public string BatchVersion { get; set; } = "v1";

    public virtual ICollection<TaskRunWaitpoint> RunsBlocked { get; set; } = new List<TaskRunWaitpoint>();

    public virtual ICollection<Waitpoint> Waitpoints { get; set; } = new List<Waitpoint>();

    public bool Sealed { get; set; } = false;

    public DateTime? SealedAt { get; set; }

    public int ExpectedCount { get; set; } = 0;

    public int CompletedCount { get; set; } = 0;

    public DateTime? CompletedAt { get; set; }

    public DateTime? ResumedAt { get; set; }

    public int ProcessingJobsCount { get; set; } = 0;

    public int ProcessingJobsExpectedCount { get; set; } = 0;

    public string? OneTimeUseToken { get; set; }

    public DateTime? ProcessingStartedAt { get; set; }

    public DateTime? ProcessingCompletedAt { get; set; }

    public int? SuccessfulRunCount { get; set; }

    public int? FailedRunCount { get; set; }

    public virtual ICollection<BatchTaskRunError> Errors { get; set; } = new List<BatchTaskRunError>();

    public virtual ICollection<BatchTaskRunItem> Items { get; set; } = new List<BatchTaskRunItem>();

    public string? TaskIdentifier { get; set; }

    public virtual CheckpointRestoreEvent? CheckpointEvent { get; set; }

    public string? CheckpointEventId { get; set; }

    public virtual TaskRunAttempt? DependentTaskAttempt { get; set; }

    public string? DependentTaskAttemptId { get; set; }

    public virtual ICollection<TaskRunDependency> RunDependencies { get; set; } = new List<TaskRunDependency>();
}
