#nullable enable

using System;

namespace LangWatch.Models;

public partial class GatewayBudgetBucketBoundary
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string BudgetId { get; set; } = null!;

    public virtual GatewayBudget Budget { get; set; } = null!;

    public string BucketScopeId { get; set; } = null!;

    public DateTime PeriodStartedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
