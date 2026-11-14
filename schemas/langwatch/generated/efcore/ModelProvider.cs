#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class ModelProvider
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string? RoutingHandle { get; set; }

    public bool Enabled { get; set; }

    public string? CustomKeys { get; set; }

    public string? ExtraHeaders { get; set; }

    public string? CustomModels { get; set; }

    public string? CustomEmbeddingsModels { get; set; }

    public string? DeploymentMapping { get; set; }

    public int? RateLimitRpm { get; set; }

    public int? RateLimitTpm { get; set; }

    public int? RateLimitRpd { get; set; }

    public GatewayProviderRotationPolicy RotationPolicy { get; set; } = GatewayProviderRotationPolicy.Manual;

    public string? ProviderConfig { get; set; }

    public int? FallbackPriorityGlobal { get; set; }

    public string? LangySkipPermissionsModels { get; set; }

    public GatewayProviderHealthStatus HealthStatus { get; set; } = GatewayProviderHealthStatus.Unknown;

    public DateTime? CircuitOpenedAt { get; set; }

    public DateTime? LastHealthCheckAt { get; set; }

    public DateTime? DisabledAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ModelProviderScope> Scopes { get; set; } = new List<ModelProviderScope>();

    public string OrganizationId { get; set; } = null!;
}
