#nullable enable

using System;

namespace Calcom.Models;

public partial class CalendarCacheEvent
{
    public string Id { get; set; } = null!;

    public string SelectedCalendarId { get; set; } = null!;

    public string ExternalId { get; set; } = null!;

    public string ExternalEtag { get; set; } = null!;

    public string? ICalUID { get; set; }

    public int ICalSequence { get; set; } = 0;

    public string? Summary { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public bool IsAllDay { get; set; } = false;

    public string? TimeZone { get; set; }

    public CalendarCacheEventStatus Status { get; set; } = CalendarCacheEventStatus.Confirmed;

    public string? RecurringEventId { get; set; }

    public DateTime? OriginalStartTime { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? ExternalCreatedAt { get; set; }

    public DateTime? ExternalUpdatedAt { get; set; }

    public virtual SelectedCalendar SelectedCalendar { get; set; } = null!;
}
