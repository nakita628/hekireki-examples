#nullable enable

using System;

namespace Trigger.Models;

public partial class IntegrationDeployment
{
    public string Id { get; set; } = null!;

    public string IntegrationName { get; set; } = null!;

    public string IntegrationDeploymentId { get; set; } = null!;

    public string CommitSHA { get; set; } = null!;

    public string? DeploymentId { get; set; }

    public string? Status { get; set; }

    public virtual WorkerDeployment? WorkerDeployment { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
