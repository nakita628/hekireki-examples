#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class ReplyMemory
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Content { get; set; } = null!;

    public ReplyMemoryKind Kind { get; set; }

    public ReplyMemoryScopeType ScopeType { get; set; }

    public string ScopeValue { get; set; } = null!;

    public bool IsLearnedStyleEvidence { get; set; } = false;

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public virtual ICollection<ReplyMemorySource> Sources { get; set; } = new List<ReplyMemorySource>();
}
