#nullable enable

using System;

namespace Trigger.Models;

public partial class TaskIdentifier
{
    public string Id { get; set; } = null!;

    public virtual RuntimeEnvironment RuntimeEnvironment { get; set; } = null!;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public TaskTriggerSource CurrentTriggerSource { get; set; } = TaskTriggerSource.Standard;

    public virtual BackgroundWorker? CurrentWorker { get; set; }

    public string? CurrentWorkerId { get; set; }

    public DateTime FirstSeenAt { get; set; }

    public DateTime LastSeenAt { get; set; }

    public bool IsInLatestDeployment { get; set; } = true;
}
