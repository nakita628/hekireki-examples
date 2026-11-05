#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class MeetingRecording
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string BotProvider { get; set; } = "recall";

    public string? ExternalBotId { get; set; }

    public string? BotName { get; set; }

    public string? ExternalRecordingId { get; set; }

    public DateTime? TranscriptRequestedAt { get; set; }

    public string? ExternalTranscriptId { get; set; }

    public string MeetingUrl { get; set; } = null!;

    public string NormalizedMeetingUrl { get; set; } = null!;

    public string? ActiveKey { get; set; }

    public DateTime MeetingStartTime { get; set; }

    public MeetingRecordingStatus Status { get; set; } = MeetingRecordingStatus.Pending;

    public string? FailureReason { get; set; }

    public string? Transcript { get; set; }

    public DateTime? TranscriptFetchedAt { get; set; }

    public DateTime? MediaDeletedAt { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
}
