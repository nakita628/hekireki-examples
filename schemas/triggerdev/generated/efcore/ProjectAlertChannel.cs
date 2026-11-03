#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class ProjectAlertChannel
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public string DeduplicationKey { get; set; } = null!;

    public bool UserProvidedDeduplicationKey { get; set; } = false;

    public virtual OrganizationIntegration? Integration { get; set; }

    public string? IntegrationId { get; set; }

    public bool Enabled { get; set; } = true;

    public ProjectAlertChannelType Type { get; set; }

    public string Name { get; set; } = null!;

    public string Properties { get; set; } = null!;

    public List<ProjectAlertType>? AlertTypes { get; set; }

    public List<RuntimeEnvironmentType>? EnvironmentTypes { get; set; } = new List<RuntimeEnvironmentType> { RuntimeEnvironmentType.Staging, RuntimeEnvironmentType.Production };

    public string? ErrorAlertConfig { get; set; }

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ProjectAlert> Alerts { get; set; } = new List<ProjectAlert>();

    public virtual ICollection<ProjectAlertStorage> AlertStorages { get; set; } = new List<ProjectAlertStorage>();
}
