#nullable enable

using System;

namespace LangWatch.Models;

public partial class ConnectedBillingAccount
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string StripeCustomerId { get; set; } = null!;

    public string? UsageSubscriptionId { get; set; }

    public string? UsageSubscriptionItemId { get; set; }

    public DateTime TermStartsAt { get; set; }

    public DateTime TermEndsAt { get; set; }

    public int CommitUsdCents { get; set; } = 0;

    public Currency SeatCurrency { get; set; }

    public int SeatRateCents { get; set; }

    public int Seats { get; set; }

    public string? BankTransferType { get; set; }

    public string? BankTransferCountry { get; set; }

    public string BillingEmail { get; set; } = null!;

    public string? PendingRenewal { get; set; }

    public string? CreatedById { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
