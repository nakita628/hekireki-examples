#nullable enable

using System;

namespace Dub.Models;

public partial class Invoice
{
    public string Id { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public string? Number { get; set; }

    public InvoiceStatus Status { get; set; } = InvoiceStatus.Processing;

    public InvoiceType Type { get; set; } = InvoiceType.DomainRenewal;

    public PaymentMethod? PaymentMethod { get; set; }

    public int Amount { get; set; } = 0;

    public int Fee { get; set; } = 0;

    public int RefundedAmount { get; set; } = 0;

    public int Total { get; set; } = 0;

    public string? ReceiptUrl { get; set; }

    public string? RefundReason { get; set; }

    public string? FailedReason { get; set; }

    public string? RegisteredDomains { get; set; }

    public string? StripeChargeMetadata { get; set; }

    public int FailedAttempts { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

    public DateTime? PaidAt { get; set; }

    public virtual Project Workspace { get; set; } = null!;
}
