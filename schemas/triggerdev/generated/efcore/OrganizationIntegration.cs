#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class OrganizationIntegration
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public IntegrationService Service { get; set; }

    public string? ExternalOrganizationId { get; set; }

    public string IntegrationData { get; set; } = null!;

    public virtual SecretReference TokenReference { get; set; } = null!;

    public string TokenReferenceId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<ProjectAlertChannel> AlertChannels { get; set; } = new List<ProjectAlertChannel>();

    public virtual ICollection<OrganizationProjectIntegration> OrganizationProjectIntegration { get; set; } = new List<OrganizationProjectIntegration>();
}
