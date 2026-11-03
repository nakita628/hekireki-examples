#nullable enable

using System;

namespace Trigger.Models;

public partial class OrganizationProjectIntegration
{
    public string Id { get; set; } = null!;

    public virtual OrganizationIntegration OrganizationIntegration { get; set; } = null!;

    public string OrganizationIntegrationId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string ExternalEntityId { get; set; } = null!;

    public string IntegrationData { get; set; } = null!;

    public string? InstalledBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
