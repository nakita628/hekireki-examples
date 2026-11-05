#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class BookingLink
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Slug { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public int DurationMinutes { get; set; }

    public int SlotIntervalMinutes { get; set; }

    public BookingLinkLocationType LocationType { get; set; } = BookingLinkLocationType.Custom;

    public string? LocationValue { get; set; }

    public int MinimumNoticeMinutes { get; set; } = 120;

    public int MaxDaysAhead { get; set; } = 90;

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public string? DestinationCalendarId { get; set; }

    public virtual Calendar? DestinationCalendar { get; set; }

    public string AvailabilityScheduleId { get; set; } = null!;

    public virtual AvailabilitySchedule AvailabilitySchedule { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
