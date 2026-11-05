#nullable enable

using System;

namespace InboxZero.Models;

public partial class Payment
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? PremiumId { get; set; }

    public virtual Premium? Premium { get; set; }

    public ProcessorType ProcessorType { get; set; } = ProcessorType.LemonSqueezy;

    public string? ProcessorId { get; set; }

    public string? ProcessorSubscriptionId { get; set; }

    public string? ProcessorCustomerId { get; set; }

    public int Amount { get; set; }

    public string Currency { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int Tax { get; set; }

    public bool TaxInclusive { get; set; }

    public bool Refunded { get; set; } = false;

    public DateTime? RefundedAt { get; set; }

    public int? RefundedAmount { get; set; }

    public string? BillingReason { get; set; }

    public DateTime? InvoiceEmailSentAt { get; set; }
}
