#nullable enable

using System;
using System.Collections.Generic;

namespace WorkoutCool.Models;

public partial class SubscriptionPlan
{
    public string Id { get; set; } = null!;

    public decimal? PriceMonthly { get; set; }

    public decimal? PriceYearly { get; set; }

    public string Currency { get; set; } = "EUR";

    public string Interval { get; set; } = "month";

    public bool IsActive { get; set; } = true;

    public List<string>? AvailableRegions { get; set; } = new List<string>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<PlanProviderMapping> ProviderMappings { get; set; } = new List<PlanProviderMapping>();
}
