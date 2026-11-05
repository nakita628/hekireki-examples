#nullable enable

using System;

namespace OpenPanel.Models;

public partial class ChatMessage
{
    public string Id { get; set; } = null!;

    public string ConversationId { get; set; } = null!;

    public virtual Conversation Conversation { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Parts { get; set; } = null!;

    public string? Metadata { get; set; }

    public DateTime CreatedAt { get; set; }
}
