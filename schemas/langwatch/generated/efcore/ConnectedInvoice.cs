#nullable enable

using System;

namespace LangWatch.Models;

public partial class ConnectedInvoice
{
    public string Id { get; set; } = null!;

    public string AccountId { get; set; } = null!;

    public string StripeInvoiceId { get; set; } = null!;

    public ConnectedInvoiceKind Kind { get; set; }

    public Currency Currency { get; set; }

    public int AmountCents { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? PaidOutOfBandAt { get; set; }

    public DateTime? TermStartsAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
