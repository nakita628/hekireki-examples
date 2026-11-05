#nullable enable

using System;

namespace InboxZero.Models;

public partial class Meeting
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string CalendarEventId { get; set; } = null!;

    public string EventTitle { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Attendees { get; set; } = null!;

    public string? OrganizerEmail { get; set; }

    public bool? JoinOverride { get; set; }

    public MeetingProcessingStatus ProcessingStatus { get; set; } = MeetingProcessingStatus.Pending;

    public int ProcessingAttempts { get; set; } = 0;

    public string? Summary { get; set; }

    public DateTime? FollowUpDraftStartedAt { get; set; }

    public string? FollowUpDraftId { get; set; }

    public DateTime? RecapSentAt { get; set; }

    public string? RecordingId { get; set; }

    public virtual MeetingRecording? Recording { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
