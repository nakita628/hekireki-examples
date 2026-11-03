#nullable enable

using System;

namespace Formbricks.Models;

public partial class OrganizationBilling
{
    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string? StripeCustomerId { get; set; }

    public string Limits { get; set; } = null!;

    public DateTime? UsageCycleAnchor { get; set; }

    public string? Stripe { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
