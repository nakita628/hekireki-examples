#nullable enable

using System;

namespace Trigger.Models;

public partial class ProjectAlert
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual RuntimeEnvironment Environment { get; set; } = null!;

    public string EnvironmentId { get; set; } = null!;

    public virtual ProjectAlertChannel Channel { get; set; } = null!;

    public string ChannelId { get; set; } = null!;

    public ProjectAlertStatus Status { get; set; } = ProjectAlertStatus.Pending;

    public ProjectAlertType Type { get; set; }

    public string? TaskRunAttemptId { get; set; }

    public string? TaskRunId { get; set; }

    public virtual WorkerDeployment? WorkerDeployment { get; set; }

    public string? WorkerDeploymentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
