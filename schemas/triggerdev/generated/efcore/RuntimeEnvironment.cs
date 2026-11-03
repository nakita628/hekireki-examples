#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class RuntimeEnvironment
{
    public string Id { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string ApiKey { get; set; } = null!;

    public DateTime? RootApiKeyHiddenAt { get; set; }

    public string PkApiKey { get; set; } = null!;

    public RuntimeEnvironmentType Type { get; set; } = RuntimeEnvironmentType.Development;

    public bool IsBranchableEnvironment { get; set; } = false;

    public string? BranchName { get; set; }

    public virtual RuntimeEnvironment? ParentEnvironment { get; set; }

    public string? ParentEnvironmentId { get; set; }

    public virtual ICollection<RuntimeEnvironment> ChildEnvironments { get; set; } = new List<RuntimeEnvironment>();

    public int? PreviewAutoArchiveAfterDays { get; set; }

    public List<string>? PreviewAutoArchiveExcludedBranches { get; set; } = new List<string>();

    public DateTime? PreviewAutoArchiveNextCheckAt { get; set; }

    public DateTime? PreviewAutoArchiveCursorCreatedAt { get; set; }

    public string? PreviewAutoArchiveCursorId { get; set; }

    public string? Git { get; set; }

    public DateTime? ArchivedAt { get; set; }

    public string Shortcode { get; set; } = null!;

    public int MaximumConcurrencyLimit { get; set; } = 5;

    public decimal ConcurrencyLimitBurstFactor { get; set; } = 2m;

    public bool Paused { get; set; } = false;

    public EnvironmentPauseSource? PauseSource { get; set; }

    public bool AutoEnableInternalSources { get; set; } = true;

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual OrgMember? OrgMember { get; set; }

    public string? OrgMemberId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? BuiltInEnvironmentVariableOverrides { get; set; }

    public string? TunnelId { get; set; }

    public virtual ICollection<BackgroundWorker> BackgroundWorkers { get; set; } = new List<BackgroundWorker>();

    public virtual ICollection<BackgroundWorkerTask> BackgroundWorkerTasks { get; set; } = new List<BackgroundWorkerTask>();

    public virtual ICollection<TaskRun> TaskRuns { get; set; } = new List<TaskRun>();

    public virtual ICollection<TaskQueue> TaskQueues { get; set; } = new List<TaskQueue>();

    public virtual ICollection<EnvironmentVariableValue> EnvironmentVariableValues { get; set; } = new List<EnvironmentVariableValue>();

    public virtual ICollection<Checkpoint> Checkpoints { get; set; } = new List<Checkpoint>();

    public virtual ICollection<WorkerDeployment> WorkerDeployments { get; set; } = new List<WorkerDeployment>();

    public virtual ICollection<WorkerDeploymentPromotion> WorkerDeploymentPromotions { get; set; } = new List<WorkerDeploymentPromotion>();

    public virtual ICollection<TaskRunAttempt> TaskRunAttempts { get; set; } = new List<TaskRunAttempt>();

    public virtual ICollection<CheckpointRestoreEvent> CheckpointRestoreEvent { get; set; } = new List<CheckpointRestoreEvent>();

    public virtual ICollection<TaskScheduleInstance> TaskScheduleInstances { get; set; } = new List<TaskScheduleInstance>();

    public virtual ICollection<ProjectAlert> Alerts { get; set; } = new List<ProjectAlert>();

    public virtual ICollection<RuntimeEnvironmentSession> Sessions { get; set; } = new List<RuntimeEnvironmentSession>();

    public virtual RuntimeEnvironmentSession? CurrentSession { get; set; }

    public string? CurrentSessionId { get; set; }

    public virtual ICollection<TaskRunNumberCounter> TaskRunNumberCounter { get; set; } = new List<TaskRunNumberCounter>();

    public virtual ICollection<TaskRunCheckpoint> TaskRunCheckpoints { get; set; } = new List<TaskRunCheckpoint>();

    public virtual ICollection<Waitpoint> Waitpoints { get; set; } = new List<Waitpoint>();

    public virtual ICollection<WorkerInstance> WorkerInstances { get; set; } = new List<WorkerInstance>();

    public virtual ICollection<WaitpointTag> WaitpointTags { get; set; } = new List<WaitpointTag>();

    public virtual ICollection<BulkActionGroup> BulkActionGroup { get; set; } = new List<BulkActionGroup>();

    public virtual ICollection<CustomerQuery> CustomerQueries { get; set; } = new List<CustomerQuery>();

    public virtual ICollection<Prompt> Prompts { get; set; } = new List<Prompt>();

    public virtual ICollection<PlaygroundConversation> PlaygroundConversations { get; set; } = new List<PlaygroundConversation>();

    public virtual ICollection<ErrorGroupState> ErrorGroupStates { get; set; } = new List<ErrorGroupState>();

    public virtual ICollection<TaskIdentifier> TaskIdentifiers { get; set; } = new List<TaskIdentifier>();

    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();

    public virtual ICollection<RevokedApiKey> RevokedApiKeys { get; set; } = new List<RevokedApiKey>();
}
