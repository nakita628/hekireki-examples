#nullable enable

using System;

namespace InboxZero.Models;

public partial class AvailabilityWindow
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int Weekday { get; set; }

    public int StartMinutes { get; set; }

    public int EndMinutes { get; set; }

    public string AvailabilityScheduleId { get; set; } = null!;

    public virtual AvailabilitySchedule AvailabilitySchedule { get; set; } = null!;
}
