#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class Organization
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CloudConfig { get; set; }

    public string? Metadata { get; set; }

    public DateTime? CloudBillingCycleAnchor { get; set; } = DateTime.UtcNow;

    public DateTime? CloudBillingCycleUpdatedAt { get; set; }

    public int? CloudCurrentCycleUsage { get; set; }

    public string? CloudFreeTierUsageThresholdState { get; set; }

    public bool AiFeaturesEnabled { get; set; } = false;

    public bool AiTelemetryEnabled { get; set; } = true;

    public List<string>? FeatureFlagOrgDefaults { get; set; } = new List<string>();

    public string? SfdcOrgId { get; set; }

    public virtual ICollection<OrganizationMembership> OrganizationMemberships { get; set; } = new List<OrganizationMembership>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<MembershipInvitation> MembershipInvitation { get; set; } = new List<MembershipInvitation>();

    public virtual ICollection<ApiKey> ApiKey { get; set; } = new List<ApiKey>();

    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();

    public virtual ICollection<CloudSpendAlert> CloudSpendAlerts { get; set; } = new List<CloudSpendAlert>();

    public virtual ICollection<VerifiedDomain> VerifiedDomains { get; set; } = new List<VerifiedDomain>();

    public virtual GatewayConfig? GatewayConfig { get; set; }

    public virtual ICollection<GatewayAiConnection> GatewayAiConnections { get; set; } = new List<GatewayAiConnection>();

    public virtual ICollection<LlmApiKeys> OrganizationLlmApiKeys { get; set; } = new List<LlmApiKeys>();

    public virtual ICollection<RoleAssignment> RoleAssignments { get; set; } = new List<RoleAssignment>();
}
