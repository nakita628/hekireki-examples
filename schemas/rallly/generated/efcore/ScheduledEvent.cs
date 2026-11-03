#nullable enable

using System;
using System.Collections.Generic;

namespace Rallly.Models;

public partial class ScheduledEvent
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string SpaceId { get; set; } = null!;

    public string? EventTypeId { get; set; }

    public string? SheetSlotId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Location { get; set; }

    public string? Conferencing { get; set; }

    public int? Capacity { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ScheduledEventStatus Status { get; set; } = ScheduledEventStatus.Confirmed;

    public string? TimeZone { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public bool AllDay { get; set; } = false;

    public DateTime? DeletedAt { get; set; }

    public int Sequence { get; set; } = 0;

    public string Uid { get; set; } = null!;

    public bool HideAttendees { get; set; } = false;

    public virtual User User { get; set; } = null!;

    public virtual Space Space { get; set; } = null!;

    public virtual EventType? EventType { get; set; }

    public virtual SheetSlot? SheetSlot { get; set; }

    public virtual ICollection<RescheduledEventDate> RescheduledDates { get; set; } = new List<RescheduledEventDate>();

    public virtual ICollection<ScheduledEventInvite> Invites { get; set; } = new List<ScheduledEventInvite>();

    public virtual ICollection<Poll> Polls { get; set; } = new List<Poll>();
}
