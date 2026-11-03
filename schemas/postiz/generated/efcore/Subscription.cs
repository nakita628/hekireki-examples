#nullable enable

using System;

namespace Postiz.Models;

public partial class Subscription
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public SubscriptionTier SubscriptionTier { get; set; }

    public string Provider { get; set; } = "stripe";

    public string? Identifier { get; set; }

    public DateTime? CancelAt { get; set; }

    public Period Period { get; set; }

    public int TotalChannels { get; set; }

    public bool IsLifetime { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;
}
