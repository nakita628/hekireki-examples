#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class Chat
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? Name { get; set; }

    public int CompactionCount { get; set; } = 0;

    public int? LastSeenRulesRevision { get; set; }

    public string? ActiveStreamId { get; set; }

    public DateTime? ActiveStreamStartedAt { get; set; }

    public virtual ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

    public virtual ICollection<ChatCompaction> Compactions { get; set; } = new List<ChatCompaction>();

    public virtual ICollection<ChatMemory> Memories { get; set; } = new List<ChatMemory>();

    public virtual ICollection<ChatActionTotal> ActionTotals { get; set; } = new List<ChatActionTotal>();

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
