#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class ScheduledEmail
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public string ClientMutationId { get; set; } = null!;

    public string PayloadHash { get; set; } = null!;

    public string Payload { get; set; } = null!;

    public string? ThreadId { get; set; }

    public DateTime SendAt { get; set; }

    public ScheduledEmailStatus Status { get; set; } = ScheduledEmailStatus.Pending;

    public DateTime? ProcessingStartedAt { get; set; }

    public DateTime? ExecutionQueuedAt { get; set; }

    public DateTime? SentAt { get; set; }

    public string? Error { get; set; }

    public DateTime? RemindAt { get; set; }

    public EmailReminderStatus ReminderStatus { get; set; } = EmailReminderStatus.None;

    public DateTime? ReminderStartedAt { get; set; }

    public bool HeldForUndo { get; set; } = false;

    public List<string>? DraftMessageIds { get; set; } = new List<string>();
}
