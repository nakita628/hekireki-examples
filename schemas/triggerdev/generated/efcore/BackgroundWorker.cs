#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class BackgroundWorker
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public RunEngineVersion Engine { get; set; } = RunEngineVersion.V1;

    public string ContentHash { get; set; } = null!;

    public string SdkVersion { get; set; } = "unknown";

    public string CliVersion { get; set; } = "unknown";

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual RuntimeEnvironment RuntimeEnvironment { get; set; } = null!;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public string Version { get; set; } = null!;

    public string Metadata { get; set; } = null!;

    public string? Runtime { get; set; }

    public string? RuntimeVersion { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<BackgroundWorkerTask> Tasks { get; set; } = new List<BackgroundWorkerTask>();

    public virtual ICollection<TaskRunAttempt> Attempts { get; set; } = new List<TaskRunAttempt>();

    public virtual ICollection<TaskRun> LockedRuns { get; set; } = new List<TaskRun>();

    public virtual ICollection<BackgroundWorkerFile> Files { get; set; } = new List<BackgroundWorkerFile>();

    public virtual ICollection<TaskQueue> Queues { get; set; } = new List<TaskQueue>();

    public virtual ICollection<PromptVersion> PromptVersions { get; set; } = new List<PromptVersion>();

    public virtual WorkerDeployment? Deployment { get; set; }

    public virtual WorkerInstanceGroup? WorkerGroup { get; set; }

    public string? WorkerGroupId { get; set; }

    public bool SupportsLazyAttempts { get; set; } = false;

    public virtual ICollection<TaskIdentifier> TaskIdentifiers { get; set; } = new List<TaskIdentifier>();
}
