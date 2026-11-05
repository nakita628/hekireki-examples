#nullable enable

using System;

namespace InboxZero.Models;

public partial class Action
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ActionType Type { get; set; }

    public string RuleId { get; set; } = null!;

    public string EmailAccountId { get; set; } = null!;

    public virtual Rule Rule { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public string? MessagingChannelId { get; set; }

    public string? MessagingChannelEmailAccountId { get; set; }

    public virtual MessagingChannel? MessagingChannel { get; set; }

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

    public int? DelayInMinutes { get; set; }

    public string? StaticAttachments { get; set; }

    public string? IntegrationName { get; set; }

    public string? IntegrationToolName { get; set; }

    public string? IntegrationArgs { get; set; }
}
