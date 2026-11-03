#nullable enable

using System;

namespace Umami.Models;

public partial class TwoFactorRateLimit
{
    public string Id { get; set; } = null!;

    public Guid UserId { get; set; }

    public int Attempts { get; set; } = 0;

    public DateTime? LockedUntil { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
