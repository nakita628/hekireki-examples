#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class VirtualKey
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public VirtualKeyStatus Status { get; set; } = VirtualKeyStatus.Active;

    public VirtualKeyPurpose Purpose { get; set; } = VirtualKeyPurpose.User;

    public string? ExternalId { get; set; }

    public string Metadata { get; set; } = "{}";

    public DateTime? DisabledAt { get; set; }

    public string? DisabledReason { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public string HashedSecret { get; set; } = null!;

    public string DisplayPrefix { get; set; } = null!;

    public string? PrincipalUserId { get; set; }

    public virtual User? PrincipalUser { get; set; }

    public string? TraceProjectId { get; set; }

    public string Config { get; set; } = "{}";

    public long Revision { get; set; } = 0L;

    public string? PreviousHashedSecret { get; set; }

    public DateTime? PreviousSecretValidUntil { get; set; }

    public DateTime? RevokedAt { get; set; }

    public string? RevokedById { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string CreatedById { get; set; } = null!;

    public DateTime? LastUsedAt { get; set; }

    public virtual ICollection<VirtualKeyScope> Scopes { get; set; } = new List<VirtualKeyScope>();

    public virtual ICollection<GatewayBudgetLedger> LedgerEntries { get; set; } = new List<GatewayBudgetLedger>();

    public string? RoutingPolicyId { get; set; }

    public virtual RoutingPolicy? RoutingPolicy { get; set; }

    public VirtualKeyRoutingMode RoutingMode { get; set; } = VirtualKeyRoutingMode.None;

    public List<string>? ConnectServices { get; set; } = new List<string>();

    public string? LicenseTokenHash { get; set; }

    public string? LicenseInstanceId { get; set; }

    public DateTime? LicenseExpiresAt { get; set; }

    public string? LicenseId { get; set; }
}
