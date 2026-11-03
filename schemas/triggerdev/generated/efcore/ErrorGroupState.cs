#nullable enable

using System;

namespace Trigger.Models;

public partial class ErrorGroupState
{
    public string Id { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual RuntimeEnvironment? Environment { get; set; }

    public string? EnvironmentId { get; set; }

    public string TaskIdentifier { get; set; } = null!;

    public string ErrorFingerprint { get; set; } = null!;

    public ErrorGroupStatus Status { get; set; } = ErrorGroupStatus.Unresolved;

    public DateTime? IgnoredUntil { get; set; }

    public int? IgnoredUntilOccurrenceRate { get; set; }

    public int? IgnoredUntilTotalOccurrences { get; set; }

    public long? IgnoredAtOccurrenceCount { get; set; }

    public DateTime? IgnoredAt { get; set; }

    public string? IgnoredReason { get; set; }

    public string? IgnoredByUserId { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public string? ResolvedInVersion { get; set; }

    public string? ResolvedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
