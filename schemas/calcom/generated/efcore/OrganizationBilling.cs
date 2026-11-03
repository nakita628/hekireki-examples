#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class OrganizationBilling
{
    public string Id { get; set; } = null!;

    public int TeamId { get; set; }

    public virtual Team Team { get; set; } = null!;

    public string SubscriptionId { get; set; } = null!;

    public string SubscriptionItemId { get; set; } = null!;

    public string CustomerId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string PlanName { get; set; } = null!;

    public DateTime? SubscriptionStart { get; set; }

    public DateTime? SubscriptionTrialEnd { get; set; }

    public DateTime? SubscriptionEnd { get; set; }

    public BillingPeriod? BillingPeriod { get; set; }

    public BillingMode BillingMode { get; set; } = BillingMode.Seats;

    public int? PricePerSeat { get; set; }

    public int? PaidSeats { get; set; }

    public virtual ICollection<MonthlyProration> MonthlyProrations { get; set; } = new List<MonthlyProration>();

    public virtual ICollection<SeatChangeLog> SeatChangeLogs { get; set; } = new List<SeatChangeLog>();

    public int? HighWaterMark { get; set; }

    public DateTime? HighWaterMarkPeriodStart { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
