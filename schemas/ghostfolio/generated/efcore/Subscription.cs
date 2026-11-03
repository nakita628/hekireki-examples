#nullable enable

using System;

namespace Ghostfolio.Models;

public partial class Subscription
{
    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public string Id { get; set; } = null!;

    public double? Price { get; set; }

    public string? StripeCheckoutSessionId { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;
}
