#nullable enable

using System;

namespace InboxZero.Models;

public partial class Schedule
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? IntervalDays { get; set; }

    public int? Occurrences { get; set; }

    public int? DaysOfWeek { get; set; }

    public DateTime? TimeOfDay { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public DateTime? LastOccurrenceAt { get; set; }

    public DateTime? NextOccurrenceAt { get; set; }
}
