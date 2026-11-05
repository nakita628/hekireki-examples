#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class DraftSendLog
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string ExecutedActionId { get; set; } = null!;

    public virtual ExecutedAction ExecutedAction { get; set; } = null!;

    public string SentMessageId { get; set; } = null!;

    public double SimilarityScore { get; set; }

    public double? BodySimilarityScore { get; set; }

    public string? BodySimilarityStatus { get; set; }

    public string? SimilarityMetadata { get; set; }

    public string? SentText { get; set; }

    public string? ReplyMemorySentText { get; set; }

    public int ReplyMemoryAttemptCount { get; set; } = 0;

    public DateTime? ReplyMemoryProcessedAt { get; set; }

    public virtual ICollection<ReplyMemorySource> ReplyMemorySources { get; set; } = new List<ReplyMemorySource>();
}
