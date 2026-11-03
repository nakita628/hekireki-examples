#nullable enable

using System;

namespace Trigger.Models;

public partial class WebhookEndpoint
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public string OpaqueId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public RuntimeEnvironmentType EnvironmentType { get; set; }

    public string EndpointTenantId { get; set; } = "";

    public string EndpointExternalRef { get; set; } = "";

    public string Source { get; set; } = null!;

    public string DeclaredId { get; set; } = null!;

    public string RoutingTargets { get; set; } = "[]";

    public string VerifierArtifact { get; set; } = null!;

    public string Metadata { get; set; } = "{}";

    public string SecretProvisioning { get; set; } = "either";

    public string? SetupPrompt { get; set; }

    public string? SigningSecretKey { get; set; }

    public WebhookEndpointStatus Status { get; set; } = WebhookEndpointStatus.Active;

    public DateTime? ManuallyDeactivatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
