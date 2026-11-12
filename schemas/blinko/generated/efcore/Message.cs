#nullable enable

using System;

namespace Blinko.Models;

public partial class Message
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public string Role { get; set; } = null!;

    public int ConversationId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Metadata { get; set; }

    public virtual Conversation Conversation { get; set; } = null!;
}
