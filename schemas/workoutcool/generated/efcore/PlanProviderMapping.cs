#nullable enable

using System;

namespace WorkoutCool.Models;

public partial class PlanProviderMapping
{
    public string Id { get; set; } = null!;

    public string PlanId { get; set; } = null!;

    public PaymentProcessor Provider { get; set; }

    public string ExternalId { get; set; } = null!;

    public string? Region { get; set; }

    public string? Metadata { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual SubscriptionPlan Plan { get; set; } = null!;
}
