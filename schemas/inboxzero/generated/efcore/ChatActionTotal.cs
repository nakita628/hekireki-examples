#nullable enable

using System;

namespace InboxZero.Models;

public partial class ChatActionTotal
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Action { get; set; } = null!;

    public int Count { get; set; } = 0;

    public string ChatId { get; set; } = null!;

    public virtual Chat Chat { get; set; } = null!;
}
