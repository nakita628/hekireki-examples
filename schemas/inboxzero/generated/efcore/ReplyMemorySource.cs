#nullable enable

using System;

namespace InboxZero.Models;

public partial class ReplyMemorySource
{
    public DateTime CreatedAt { get; set; }

    public DateTime? LearnedWritingStyleAnalyzedAt { get; set; }

    public string ReplyMemoryId { get; set; } = null!;

    public virtual ReplyMemory ReplyMemory { get; set; } = null!;

    public string DraftSendLogId { get; set; } = null!;

    public virtual DraftSendLog DraftSendLog { get; set; } = null!;
}
