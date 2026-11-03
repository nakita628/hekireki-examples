#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class WorkerDeployment
{
    public string Id { get; set; } = null!;

    public string ContentHash { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public string ShortCode { get; set; } = null!;

    public string Version { get; set; } = null!;

    public string? Runtime { get; set; }

    public string? RuntimeVersion { get; set; }

    public string? CliVersion { get; set; }

    public string? ImageReference { get; set; }

    public string ImagePlatform { get; set; } = "linux/amd64";

    public string? TemplateKey { get; set; }

    public string? IndexObjectKey { get; set; }

    public string? IndexSha256 { get; set; }

    public int? IndexFormatVersion { get; set; }

    public string? ExternalBuildData { get; set; }

    public string? BuildServerMetadata { get; set; }

    public string? BuildEnvVars { get; set; }

    public WorkerDeploymentStatus Status { get; set; } = WorkerDeploymentStatus.Pending;

    public WorkerDeploymentType Type { get; set; } = WorkerDeploymentType.V1;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual RuntimeEnvironment Environment { get; set; } = null!;

    public string EnvironmentId { get; set; } = null!;

    public virtual BackgroundWorker? Worker { get; set; }

    public string? WorkerId { get; set; }

    public virtual User? TriggeredBy { get; set; }

    public string? TriggeredById { get; set; }

    public string? TriggeredVia { get; set; }

    public string? CommitSHA { get; set; }

    public string? ExternalId { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? InstalledAt { get; set; }

    public DateTime? BuiltAt { get; set; }

    public DateTime? DeployedAt { get; set; }

    public DateTime? FailedAt { get; set; }

    public string? ErrorData { get; set; }

    public DateTime? CanceledAt { get; set; }

    public string? CanceledReason { get; set; }

    public string? Git { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<WorkerDeploymentPromotion> Promotions { get; set; } = new List<WorkerDeploymentPromotion>();

    public virtual ICollection<ProjectAlert> Alerts { get; set; } = new List<ProjectAlert>();

    public virtual ICollection<WorkerInstance> WorkerInstance { get; set; } = new List<WorkerInstance>();

    public virtual ICollection<IntegrationDeployment> IntegrationDeployments { get; set; } = new List<IntegrationDeployment>();
}
