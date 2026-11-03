#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class MonthlyProration
{
    public string Id { get; set; } = null!;

    public int TeamId { get; set; }

    public virtual Team Team { get; set; } = null!;

    public string MonthKey { get; set; } = null!;

    public DateTime PeriodStart { get; set; }

    public DateTime PeriodEnd { get; set; }

    public int SeatsAtStart { get; set; }

    public int SeatsAdded { get; set; }

    public int SeatsRemoved { get; set; }

    public int NetSeatIncrease { get; set; }

    public int SeatsAtEnd { get; set; }

    public string SubscriptionId { get; set; } = null!;

    public string SubscriptionItemId { get; set; } = null!;

    public string CustomerId { get; set; } = null!;

    public DateTime SubscriptionStart { get; set; }

    public DateTime SubscriptionEnd { get; set; }

    public int RemainingDays { get; set; }

    public int PricePerSeat { get; set; }

    public int ProratedAmount { get; set; }

    public string? InvoiceItemId { get; set; }

    public string? InvoiceId { get; set; }

    public string? InvoiceUrl { get; set; }

    public ProrationStatus Status { get; set; } = ProrationStatus.Pending;

    public DateTime? ChargedAt { get; set; }

    public DateTime? FailedAt { get; set; }

    public string? FailureReason { get; set; }

    public int RetryCount { get; set; } = 0;

    public virtual ICollection<SeatChangeLog> SeatChanges { get; set; } = new List<SeatChangeLog>();

    public string? Metadata { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? TeamBillingId { get; set; }

    public virtual TeamBilling? TeamBilling { get; set; }

    public string? OrganizationBillingId { get; set; }

    public virtual OrganizationBilling? OrganizationBilling { get; set; }
}
