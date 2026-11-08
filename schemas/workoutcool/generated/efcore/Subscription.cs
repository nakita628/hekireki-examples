#nullable enable

using System;

namespace WorkoutCool.Models;

public partial class Subscription
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string PlanId { get; set; } = null!;

    public virtual SubscriptionPlan Plan { get; set; } = null!;

    public string? RevenueCatUserId { get; set; }

    public SubscriptionStatus Status { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime? CurrentPeriodEnd { get; set; }

    public DateTime? CancelledAt { get; set; }

    public Platform? Platform { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
