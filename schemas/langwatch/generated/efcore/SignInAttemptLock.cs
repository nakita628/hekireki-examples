#nullable enable

using System;

namespace LangWatch.Models;

public partial class SignInAttemptLock
{
    public string Id { get; set; } = null!;

    public string IdentifierHash { get; set; } = null!;

    public int FailedCount { get; set; } = 0;

    public DateTime? LockedUntil { get; set; }

    public int ConsecutiveLockouts { get; set; } = 0;

    public bool HeldForReview { get; set; } = false;

    public string? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
