#nullable enable

using System;

namespace Calcom.Models;

public partial class Task
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime ScheduledAt { get; set; }

    public DateTime? SucceededAt { get; set; }

    public string Type { get; set; } = null!;

    public string Payload { get; set; } = null!;

    public int Attempts { get; set; } = 0;

    public int MaxAttempts { get; set; } = 3;

    public string? LastError { get; set; }

    public DateTime? LastFailedAttemptAt { get; set; }

    public string? ReferenceUid { get; set; }
}
