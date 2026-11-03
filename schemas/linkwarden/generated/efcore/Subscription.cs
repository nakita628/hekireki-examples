#nullable enable

using System;
using System.Collections.Generic;

namespace Linkwarden.Models;

public partial class Subscription
{
    public int Id { get; set; }

    public bool Active { get; set; }

    public SubscriptionProvider Provider { get; set; }

    public DateTime CurrentPeriodStart { get; set; }

    public DateTime CurrentPeriodEnd { get; set; }

    public int Quantity { get; set; } = 1;

    public string? StoreOriginalTransactionId { get; set; }

    public string? StoreProductId { get; set; }

    public string? GooglePurchaseToken { get; set; }

    public string? StoreMetadata { get; set; }

    public string? StripeSubscriptionId { get; set; }

    public virtual User User { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<User> ChildUsers { get; set; } = new List<User>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
