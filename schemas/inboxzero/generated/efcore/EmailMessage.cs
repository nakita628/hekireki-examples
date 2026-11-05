#nullable enable

using System;

namespace InboxZero.Models;

public partial class EmailMessage
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ThreadId { get; set; } = null!;

    public string MessageId { get; set; } = null!;

    public DateTime Date { get; set; }

    public string From { get; set; } = null!;

    public string? FromName { get; set; }

    public string FromDomain { get; set; } = null!;

    public string To { get; set; } = null!;

    public string? UnsubscribeLink { get; set; }

    public bool Read { get; set; }

    public bool Sent { get; set; }

    public bool Draft { get; set; }

    public bool Inbox { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
