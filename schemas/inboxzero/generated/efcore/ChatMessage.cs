#nullable enable

using System;

namespace InboxZero.Models;

public partial class ChatMessage
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Role { get; set; } = null!;

    public string Parts { get; set; } = null!;

    public string? Metadata { get; set; }

    public string ChatId { get; set; } = null!;

    public virtual Chat Chat { get; set; } = null!;
}
