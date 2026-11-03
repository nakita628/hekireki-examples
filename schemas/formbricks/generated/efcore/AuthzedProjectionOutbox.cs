#nullable enable

using System;

namespace Formbricks.Models;

public partial class AuthzedProjectionOutbox
{
    public string Id { get; set; } = null!;

    public string TargetType { get; set; } = null!;

    public string PrimaryId { get; set; } = null!;

    public string? SecondaryId { get; set; }

    public bool IsRevocation { get; set; } = false;

    public int Attempts { get; set; } = 0;

    public int PermanentFailures { get; set; } = 0;

    public DateTime AvailableAt { get; set; }

    public DateTime? LeasedAt { get; set; }

    public DateTime? LeaseExpiresAt { get; set; }

    public string? LeaseOwner { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public DateTime? DeadLetteredAt { get; set; }

    public DateTime? LastAttemptAt { get; set; }

    public string? LastErrorCode { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
