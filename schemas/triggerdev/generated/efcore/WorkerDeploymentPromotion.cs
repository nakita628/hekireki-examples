#nullable enable

namespace Trigger.Models;

public partial class WorkerDeploymentPromotion
{
    public string Id { get; set; } = null!;

    public string Label { get; set; } = null!;

    public virtual WorkerDeployment Deployment { get; set; } = null!;

    public string DeploymentId { get; set; } = null!;

    public virtual RuntimeEnvironment Environment { get; set; } = null!;

    public string EnvironmentId { get; set; } = null!;
}
