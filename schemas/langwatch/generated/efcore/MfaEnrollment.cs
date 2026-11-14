#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class MfaEnrollment
{
    public string UserId { get; set; } = null!;

    public string? EnrollmentId { get; set; }

    public string? Method { get; set; }

    public string State { get; set; } = null!;

    public DateTime? EnrolledAt { get; set; }

    public DateTime? ConfirmedAt { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public DateTime? DisabledAt { get; set; }

    public string? DisabledVia { get; set; }

    public int BackupCodeCount { get; set; } = 0;

    public List<int>? ConsumedBackupCodeIndexes { get; set; } = new List<int>();

    public int FailedCount { get; set; } = 0;

    public DateTime OccurredAt { get; set; }

    public string LastEventId { get; set; } = null!;

    public DateTime AcceptedAt { get; set; }

    public string ProjectionVersion { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
