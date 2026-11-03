#nullable enable

using System;

namespace Documenso.Models;

public partial class Subscription
{
    public int Id { get; set; }

    public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Inactive;

    public string PlanId { get; set; } = null!;

    public string PriceId { get; set; } = null!;

    public DateTime? PeriodEnd { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool CancelAtPeriodEnd { get; set; } = false;

    public string CustomerId { get; set; } = null!;

    public string OrganisationId { get; set; } = null!;

    public virtual Organisation Organisation { get; set; } = null!;
}
