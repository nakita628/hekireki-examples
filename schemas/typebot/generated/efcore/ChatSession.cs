#nullable enable

using System;

namespace Typebot.Models;

public partial class ChatSession
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string State { get; set; } = null!;

    public bool? IsReplying { get; set; }
}
