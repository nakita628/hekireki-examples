#nullable enable

using System;

namespace InboxZero.Models;

public partial class SentMessageOpen
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Token { get; set; } = null!;

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public string? ThreadId { get; set; }

    public string? MessageId { get; set; }

    public DateTime? FirstOpenedAt { get; set; }

    public DateTime? LastOpenedAt { get; set; }

    public int OpenCount { get; set; } = 0;
}
