#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class ExecutedAction
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ActionType Type { get; set; }

    public ExecutedActionStatus? ExecutionStatus { get; set; }

    public DateTime? ExecutionStartedAt { get; set; }

    public DateTime? ExecutedAt { get; set; }

    public string? ExecutionError { get; set; }

    public List<string>? SentMessageIds { get; set; } = new List<string>();

    public string ExecutedRuleId { get; set; } = null!;

    public virtual ExecutedRule ExecutedRule { get; set; } = null!;

    public string? MessagingChannelId { get; set; }

    public virtual MessagingChannel? MessagingChannel { get; set; }

    public string? MessagingMessageId { get; set; }

    public DateTime? MessagingMessageSentAt { get; set; }

    public MessagingMessageStatus? MessagingMessageStatus { get; set; }

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

    public string? StaticAttachments { get; set; }

    public string? SelectedAttachments { get; set; }

    public string? IntegrationName { get; set; }

    public string? IntegrationToolName { get; set; }

    public string? IntegrationArgs { get; set; }

    public string? DraftId { get; set; }

    public DraftEmailStatus? DraftStatus { get; set; }

    public bool? WasDraftSent { get; set; }

    public string? DraftModelProvider { get; set; }

    public string? DraftModelName { get; set; }

    public int? DraftPipelineVersion { get; set; }

    public string? DraftContextMetadata { get; set; }

    public virtual DraftSendLog? DraftSendLog { get; set; }

    public virtual ICollection<DigestItem> DigestItems { get; set; } = new List<DigestItem>();

    public virtual ScheduledAction? ScheduledAction { get; set; }
}
