#nullable enable

using System;

namespace InboxZero.Models;

public partial class ChatMemory
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Content { get; set; } = null!;

    public string? ChatId { get; set; }

    public virtual Chat? Chat { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
