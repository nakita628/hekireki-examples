#nullable enable

using System;

namespace InboxZero.Models;

public partial class ThreadTracker
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime SentAt { get; set; }

    public string ThreadId { get; set; } = null!;

    public string MessageId { get; set; } = null!;

    public bool Resolved { get; set; } = false;

    public ThreadTrackerType Type { get; set; }

    public DateTime? FollowUpAppliedAt { get; set; }

    public string? FollowUpDraftId { get; set; }

    public string? FollowUpDraftContent { get; set; }

    public DateTime? FollowUpDraftCreatedAt { get; set; }

    public string? FollowUpNotifications { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
