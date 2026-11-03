#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class TaskRun
{
    public string Id { get; set; } = null!;

    public int Number { get; set; } = 0;

    public string FriendlyId { get; set; } = null!;

    public RunEngineVersion Engine { get; set; } = RunEngineVersion.V1;

    public TaskRunStatus Status { get; set; } = TaskRunStatus.Pending;

    public string? StatusReason { get; set; }

    public string? IdempotencyKey { get; set; }

    public DateTime? IdempotencyKeyExpiresAt { get; set; }

    public string? IdempotencyKeyOptions { get; set; }

    public string? Debounce { get; set; }

    public string TaskIdentifier { get; set; } = null!;

    public bool IsTest { get; set; } = false;

    public string Payload { get; set; } = null!;

    public string PayloadType { get; set; } = "application/json";

    public string? Context { get; set; }

    public string? TraceContext { get; set; }

    public string TraceId { get; set; } = null!;

    public string SpanId { get; set; } = null!;

    public virtual RuntimeEnvironment RuntimeEnvironment { get; set; } = null!;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public RuntimeEnvironmentType? EnvironmentType { get; set; }

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string? OrganizationId { get; set; }

    public string Queue { get; set; } = null!;

    public string? LockedQueueId { get; set; }

    public string WorkerQueue { get; set; } = "main";

    public string? Region { get; set; }

    public string? SecondaryMasterQueue { get; set; }

    public int? AttemptNumber { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<TaskRunAttempt> Attempts { get; set; } = new List<TaskRunAttempt>();

    public List<string>? RunTags { get; set; }

    public string? TaskVersion { get; set; }

    public string? SdkVersion { get; set; }

    public string? CliVersion { get; set; }

    public virtual ICollection<Checkpoint> Checkpoints { get; set; } = new List<Checkpoint>();

    public DateTime? StartedAt { get; set; }

    public DateTime? ExecutedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? MachinePreset { get; set; }

    public int UsageDurationMs { get; set; } = 0;

    public double CostInCents { get; set; } = 0.0;

    public double BaseCostInCents { get; set; } = 0.0;

    public DateTime? LockedAt { get; set; }

    public virtual BackgroundWorkerTask? LockedBy { get; set; }

    public string? LockedById { get; set; }

    public virtual BackgroundWorker? LockedToVersion { get; set; }

    public string? LockedToVersionId { get; set; }

    public int PriorityMs { get; set; } = 0;

    public string? ConcurrencyKey { get; set; }

    public string? Gates { get; set; }

    public DateTime? DelayUntil { get; set; }

    public DateTime? QueuedAt { get; set; }

    public string? Ttl { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public int? MaxAttempts { get; set; }

    public string? LockedRetryConfig { get; set; }

    public string? OneTimeUseToken { get; set; }

    public virtual Waitpoint? AssociatedWaitpoint { get; set; }

    public virtual ICollection<TaskRunWaitpoint> BlockedByWaitpoints { get; set; } = new List<TaskRunWaitpoint>();

    public virtual ICollection<Waitpoint> ConnectedWaitpoints { get; set; } = new List<Waitpoint>();

    public string TaskEventStore { get; set; } = "taskEvent";

    public DateTime? QueueTimestamp { get; set; }

    public virtual ICollection<BatchTaskRunItem> BatchItems { get; set; } = new List<BatchTaskRunItem>();

    public virtual TaskRunDependency? Dependency { get; set; }

    public virtual ICollection<CheckpointRestoreEvent> CheckpointRestoreEvent { get; set; } = new List<CheckpointRestoreEvent>();

    public virtual ICollection<TaskRunExecutionSnapshot> ExecutionSnapshots { get; set; } = new List<TaskRunExecutionSnapshot>();

    public string? ScheduleInstanceId { get; set; }

    public string? ScheduleId { get; set; }

    public virtual ICollection<BulkActionItem> SourceBulkActionItems { get; set; } = new List<BulkActionItem>();

    public virtual ICollection<BulkActionItem> DestinationBulkActionItems { get; set; } = new List<BulkActionItem>();

    public List<string>? BulkActionGroupIds { get; set; } = new List<string>();

    public DateTime? LogsDeletedAt { get; set; }

    public string? ReplayedFromTaskRunFriendlyId { get; set; }

    public virtual TaskRun? RootTaskRun { get; set; }

    public string? RootTaskRunId { get; set; }

    public virtual ICollection<TaskRun> DescendantRuns { get; set; } = new List<TaskRun>();

    public virtual TaskRun? ParentTaskRun { get; set; }

    public string? ParentTaskRunId { get; set; }

    public virtual ICollection<TaskRun> ChildRuns { get; set; } = new List<TaskRun>();

    public virtual TaskRunAttempt? ParentTaskRunAttempt { get; set; }

    public string? ParentTaskRunAttemptId { get; set; }

    public virtual BatchTaskRun? Batch { get; set; }

    public string? BatchId { get; set; }

    public bool ResumeParentOnCompletion { get; set; } = false;

    public int Depth { get; set; } = 0;

    public string? ParentSpanId { get; set; }

    public string? RunChainState { get; set; }

    public string? SeedMetadata { get; set; }

    public string SeedMetadataType { get; set; } = "application/json";

    public string? Metadata { get; set; }

    public string MetadataType { get; set; } = "application/json";

    public int MetadataVersion { get; set; } = 1;

    public string? Annotations { get; set; }

    public bool? IsWarmStart { get; set; }

    public string? Output { get; set; }

    public string OutputType { get; set; } = "application/json";

    public string? Error { get; set; }

    public string? PlanType { get; set; }

    public int? MaxDurationInSeconds { get; set; }

    public string RealtimeStreamsVersion { get; set; } = "v1";

    public List<string>? RealtimeStreams { get; set; } = new List<string>();

    public string? StreamBasinName { get; set; }

    public virtual ICollection<PlaygroundConversation> PlaygroundConversations { get; set; } = new List<PlaygroundConversation>();
}
