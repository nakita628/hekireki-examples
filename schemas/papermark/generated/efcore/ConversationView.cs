#nullable enable

using System;

namespace Papermark.Models;

public partial class ConversationView
{
    public string Id { get; set; } = null!;

    public string ConversationId { get; set; } = null!;

    public virtual Conversation Conversation { get; set; } = null!;

    public string ViewId { get; set; } = null!;

    public virtual View View { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
