#nullable enable

using System;

namespace InboxZero.Models;

public partial class Referral
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ReferrerUserId { get; set; } = null!;

    public virtual User ReferrerUser { get; set; } = null!;

    public string ReferredUserId { get; set; } = null!;

    public virtual User ReferredUser { get; set; } = null!;

    public string ReferralCodeUsed { get; set; } = null!;

    public ReferralStatus Status { get; set; } = ReferralStatus.Pending;

    public DateTime? RewardGrantedAt { get; set; }

    public string? StripeBalanceTransactionId { get; set; }

    public int? RewardAmount { get; set; }
}
