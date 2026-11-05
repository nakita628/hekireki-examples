#nullable enable

using System;

namespace InboxZero.Models;

public partial class PendingPremiumGrant
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Email { get; set; } = null!;

    public PremiumTier Tier { get; set; }

    public int Count { get; set; } = 1;

    public int? EmailAccountsAccess { get; set; }
}
