#nullable enable

using System;

namespace OpenPanel.Models;

public partial class TwoFactor
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string Secret { get; set; } = null!;

    public string BackupCodes { get; set; } = null!;

    public bool Verified { get; set; } = true;

    public int FailedVerificationCount { get; set; } = 0;

    public DateTime? LockedUntil { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
