#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class Project
{
    public string Id { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ExternalRef { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime? InitializedAt { get; set; }

    public string? DefaultRuntime { get; set; }

    public ProjectVersion Version { get; set; } = ProjectVersion.V2;

    public RunEngineVersion Engine { get; set; } = RunEngineVersion.V1;

    public string? BuilderProjectId { get; set; }

    public virtual ICollection<WorkerInstanceGroup> WorkerGroups { get; set; } = new List<WorkerInstanceGroup>();

    public virtual ICollection<WorkerInstance> Workers { get; set; } = new List<WorkerInstance>();

    public virtual WorkerInstanceGroup? DefaultWorkerGroup { get; set; }

    public string? DefaultWorkerGroupId { get; set; }

    public List<string>? AllowedWorkerQueues { get; set; } = new List<string>();

    public virtual ICollection<RuntimeEnvironment> Environments { get; set; } = new List<RuntimeEnvironment>();

    public virtual ICollection<BackgroundWorker> BackgroundWorkers { get; set; } = new List<BackgroundWorker>();

    public virtual ICollection<BackgroundWorkerTask> BackgroundWorkerTasks { get; set; } = new List<BackgroundWorkerTask>();

    public virtual ICollection<TaskRun> TaskRuns { get; set; } = new List<TaskRun>();

    public virtual ICollection<TaskRunTag> RunTags { get; set; } = new List<TaskRunTag>();

    public virtual ICollection<TaskQueue> TaskQueues { get; set; } = new List<TaskQueue>();

    public virtual ICollection<EnvironmentVariable> EnvironmentVariables { get; set; } = new List<EnvironmentVariable>();

    public virtual ICollection<Checkpoint> Checkpoints { get; set; } = new List<Checkpoint>();

    public virtual ICollection<WorkerDeployment> WorkerDeployment { get; set; } = new List<WorkerDeployment>();

    public virtual ICollection<CheckpointRestoreEvent> CheckpointRestoreEvent { get; set; } = new List<CheckpointRestoreEvent>();

    public virtual ICollection<TaskSchedule> TaskSchedules { get; set; } = new List<TaskSchedule>();

    public virtual ICollection<ProjectAlertChannel> AlertChannels { get; set; } = new List<ProjectAlertChannel>();

    public virtual ICollection<ProjectAlert> Alerts { get; set; } = new List<ProjectAlert>();

    public virtual ICollection<ProjectAlertStorage> AlertStorages { get; set; } = new List<ProjectAlertStorage>();

    public virtual ICollection<BulkActionGroup> BulkActionGroups { get; set; } = new List<BulkActionGroup>();

    public virtual ICollection<BackgroundWorkerFile> BackgroundWorkerFile { get; set; } = new List<BackgroundWorkerFile>();

    public virtual ICollection<Waitpoint> Waitpoints { get; set; } = new List<Waitpoint>();

    public virtual ICollection<TaskRunWaitpoint> TaskRunWaitpoints { get; set; } = new List<TaskRunWaitpoint>();

    public virtual ICollection<TaskRunCheckpoint> TaskRunCheckpoints { get; set; } = new List<TaskRunCheckpoint>();

    public virtual ICollection<WaitpointTag> WaitpointTags { get; set; } = new List<WaitpointTag>();

    public virtual ConnectedGithubRepository? ConnectedGithubRepository { get; set; }

    public virtual TrustedDeployRepository? TrustedDeployRepository { get; set; }

    public virtual ICollection<OrganizationProjectIntegration> OrganizationProjectIntegration { get; set; } = new List<OrganizationProjectIntegration>();

    public virtual ICollection<CustomerQuery> CustomerQueries { get; set; } = new List<CustomerQuery>();

    public virtual ICollection<PlaygroundConversation> PlaygroundConversations { get; set; } = new List<PlaygroundConversation>();

    public string? BuildSettings { get; set; }

    public string? OnboardingData { get; set; }

    public virtual ICollection<TaskScheduleInstance> TaskScheduleInstances { get; set; } = new List<TaskScheduleInstance>();

    public virtual ICollection<MetricsDashboard> MetricsDashboards { get; set; } = new List<MetricsDashboard>();

    public virtual ICollection<LlmModel> LlmModels { get; set; } = new List<LlmModel>();

    public virtual ICollection<Prompt> Prompts { get; set; } = new List<Prompt>();

    public virtual ICollection<PlatformNotification> PlatformNotifications { get; set; } = new List<PlatformNotification>();

    public virtual ICollection<ErrorGroupState> ErrorGroupStates { get; set; } = new List<ErrorGroupState>();

    public virtual ICollection<TaskIdentifier> TaskIdentifiers { get; set; } = new List<TaskIdentifier>();
}
