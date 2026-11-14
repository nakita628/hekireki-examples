#nullable enable

using System;

namespace Rallly.Models;

public partial class Subscription
{
    public string Id { get; set; } = null!;

    public string PriceId { get; set; } = null!;

    public int Quantity { get; set; } = 1;

    public string SubscriptionItemId { get; set; } = null!;

    public int Amount { get; set; }

    public double? DiscountPercentOff { get; set; }

    public int? DiscountAmountOff { get; set; }

    public SubscriptionStatus Status { get; set; }

    public bool Active { get; set; }

    public string Currency { get; set; } = null!;

    public SubscriptionInterval Interval { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime PeriodStart { get; set; }

    public DateTime PeriodEnd { get; set; }

    public bool CancelAtPeriodEnd { get; set; } = false;

    public string UserId { get; set; } = null!;

    public string SpaceId { get; set; } = null!;

    public string? ApiAddonItemId { get; set; }

    public string? ApiAddonPriceId { get; set; }

    public int? ApiAddonAmount { get; set; }

    public string? ApiAddonTier { get; set; }

    public string? ApiPollRetainedTier { get; set; }

    public DateTime? ApiPollRetainedUntil { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Space Space { get; set; } = null!;
}
