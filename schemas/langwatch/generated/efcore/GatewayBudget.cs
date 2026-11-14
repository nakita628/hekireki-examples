#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class GatewayBudget
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public GatewayBudgetScopeType ScopeType { get; set; }

    public string ScopeId { get; set; } = null!;

    public string? ProviderKey { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public GatewayBudgetWindow Window { get; set; }

    public decimal LimitUsd { get; set; }

    public GatewayBudgetBreachAction OnBreach { get; set; } = GatewayBudgetBreachAction.Block;

    public string? Timezone { get; set; }

    public string? ExternalId { get; set; }

    public string Metadata { get; set; } = "{}";

    public decimal SpentUsd { get; set; } = 0m;

    public DateTime CurrentPeriodStartedAt { get; set; }

    public DateTime ResetsAt { get; set; }

    public DateTime? LastResetAt { get; set; }

    public DateTime? CycleAnchorAt { get; set; }

    public DateTime? ArchivedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string CreatedById { get; set; } = null!;

    public virtual User CreatedBy { get; set; } = null!;

    public virtual ICollection<GatewayBudgetLedger> LedgerEntries { get; set; } = new List<GatewayBudgetLedger>();

    public virtual ICollection<GatewayBudgetBucketBoundary> BucketBoundaries { get; set; } = new List<GatewayBudgetBucketBoundary>();

    public string? ManagedByVirtualKeyId { get; set; }
}
