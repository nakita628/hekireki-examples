#nullable enable

using System;

namespace Langfuse.Models;

public partial class GatewayAiConnection
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string Name { get; set; } = null!;

    public GatewayProvider Provider { get; set; }

    public string EncryptedCredential { get; set; } = null!;

    public string DisplaySecret { get; set; } = null!;

    public string? CreatedById { get; set; }

    public GatewayConnectionStatus Status { get; set; } = GatewayConnectionStatus.Enabled;

    public string? AllowedModels { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
