#nullable enable

using System;

namespace InboxZero.Models;

public partial class MeetingBriefing
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CalendarEventId { get; set; } = null!;

    public string EventTitle { get; set; } = null!;

    public DateTime EventStartTime { get; set; }

    public int GuestCount { get; set; }

    public MeetingBriefingStatus Status { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
