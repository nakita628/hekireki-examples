#nullable enable

using System;

namespace InboxZero.Models;

public partial class ResponseTime
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string ThreadId { get; set; } = null!;

    public string SentMessageId { get; set; } = null!;

    public string ReceivedMessageId { get; set; } = null!;

    public int ResponseTimeMins { get; set; }

    public DateTime ReceivedAt { get; set; }

    public DateTime SentAt { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
