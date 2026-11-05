#nullable enable

using System;

namespace InboxZero.Models;

public partial class ChatCompaction
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Summary { get; set; } = null!;

    public int MessageCount { get; set; }

    public DateTime CompactedBeforeCreatedAt { get; set; }

    public string ChatId { get; set; } = null!;

    public virtual Chat Chat { get; set; } = null!;
}
