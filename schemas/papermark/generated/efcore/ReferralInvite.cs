#nullable enable

using System;

namespace Papermark.Models;

public partial class ReferralInvite
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string? TeamId { get; set; }

    public virtual Team? Team { get; set; }

    public string ScopeId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? ReferralLink { get; set; }

    public DateTime SentAt { get; set; }
}
