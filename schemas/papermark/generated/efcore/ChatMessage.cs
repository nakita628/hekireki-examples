#nullable enable

using System;

namespace Papermark.Models;

public partial class ChatMessage
{
    public string Id { get; set; } = null!;

    public string ChatId { get; set; } = null!;

    public virtual Chat Chat { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? Metadata { get; set; }

    public DateTime CreatedAt { get; set; }
}
