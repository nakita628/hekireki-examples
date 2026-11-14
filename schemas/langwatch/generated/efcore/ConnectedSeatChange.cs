#nullable enable

using System;

namespace LangWatch.Models;

public partial class ConnectedSeatChange
{
    public string Id { get; set; } = null!;

    public string LicenseId { get; set; } = null!;

    public string? AccountId { get; set; }

    public string? OrganizationId { get; set; }

    public DateTime ChangedAt { get; set; }

    public int AddedSeats { get; set; }

    public int UnitAmountCents { get; set; }

    public int AmountCents { get; set; }

    public Currency? Currency { get; set; }

    public string? StripeInvoiceId { get; set; }

    public ConnectedSeatChangeState State { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
