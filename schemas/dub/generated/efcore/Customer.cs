#nullable enable

using System;

namespace Dub.Models;

public partial class Customer
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public string? Avatar { get; set; }

    public string? ExternalId { get; set; }

    public string? StripeCustomerId { get; set; }

    public string? LinkId { get; set; }

    public string? ClickId { get; set; }

    public DateTime? ClickedAt { get; set; }

    public string? Country { get; set; }

    public int Sales { get; set; } = 0;

    public long SaleAmount { get; set; } = 0L;

    public DateTime? FirstSaleAt { get; set; }

    public DateTime? SubscriptionCanceledAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public string? ProjectConnectId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual Link? Link { get; set; }
}
