#nullable enable

using System;

namespace LangWatch.Models;

public partial class LicenseRenewalClaim
{
    public string StripeInvoiceId { get; set; } = null!;

    public string StripeSubscriptionId { get; set; } = null!;

    public string ClaimToken { get; set; } = null!;

    public DateTime ClaimedAt { get; set; }

    public string? IssuedLicense { get; set; }

    public DateTime? DeliveredAt { get; set; }

    public string? LicenseId { get; set; }
}
