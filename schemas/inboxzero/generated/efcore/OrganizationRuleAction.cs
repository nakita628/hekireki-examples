#nullable enable

using System;

namespace InboxZero.Models;

public partial class OrganizationRuleAction
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string OrganizationRuleId { get; set; } = null!;

    public virtual OrganizationRule OrganizationRule { get; set; } = null!;

    public ActionType Type { get; set; }

    public string? Label { get; set; }

    public string? Subject { get; set; }

    public string? Content { get; set; }

    public string? To { get; set; }

    public string? Cc { get; set; }

    public string? Bcc { get; set; }

    public string? Url { get; set; }

    public string? FolderName { get; set; }

    public int? DelayInMinutes { get; set; }

    public string? StaticAttachments { get; set; }
}
