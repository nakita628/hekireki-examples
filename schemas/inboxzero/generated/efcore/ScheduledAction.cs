#nullable enable

using System;

namespace InboxZero.Models;

public partial class ScheduledAction
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ExecutedRuleId { get; set; } = null!;

    public ActionType ActionType { get; set; }

    public string MessageId { get; set; } = null!;

    public string ThreadId { get; set; } = null!;

    public DateTime ScheduledFor { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public ScheduledActionStatus Status { get; set; } = ScheduledActionStatus.Pending;

    public SchedulingStatus SchedulingStatus { get; set; } = SchedulingStatus.Pending;

    public string? Label { get; set; }

    public string? LabelId { get; set; }

    public string? Subject { get; set; }

    public string? Content { get; set; }

    public string? To { get; set; }

    public string? Cc { get; set; }

    public string? Bcc { get; set; }

    public string? Url { get; set; }

    public string? FolderName { get; set; }

    public string? FolderId { get; set; }

    public string? ScheduledId { get; set; }

    public string? StaticAttachments { get; set; }

    public string? SelectedAttachments { get; set; }

    public DateTime? ExecutedAt { get; set; }

    public string? ExecutedActionId { get; set; }

    public virtual ExecutedRule ExecutedRule { get; set; } = null!;

    public virtual ExecutedAction? ExecutedAction { get; set; }

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
