#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class BulkInboxActionJob
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public BulkInboxActionJobStatus Status { get; set; } = BulkInboxActionJobStatus.Running;

    public string Search { get; set; } = null!;

    public string Action { get; set; } = null!;

    public string? LabelId { get; set; }

    public string? LabelName { get; set; }

    public string? Instruction { get; set; }

    public string? PageToken { get; set; }

    public DateTime? RateLimitedUntil { get; set; }

    public DateTime? LeaseExpiresAt { get; set; }

    public string? ChunkToken { get; set; }

    public int RecoveryCount { get; set; } = 0;

    public int ProcessedCount { get; set; } = 0;

    public int MatchedCount { get; set; } = 0;

    public int ActedCount { get; set; } = 0;

    public int SkippedCount { get; set; } = 0;

    public int FailedCount { get; set; } = 0;

    public bool LimitReached { get; set; } = false;

    public string? Error { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string ConfirmationKey { get; set; } = null!;

    public string? ChatId { get; set; }

    public virtual Chat? Chat { get; set; }

    public virtual ICollection<BulkInboxActionThread> HandledThreads { get; set; } = new List<BulkInboxActionThread>();

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
