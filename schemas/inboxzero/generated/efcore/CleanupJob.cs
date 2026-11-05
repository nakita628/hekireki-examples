#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class CleanupJob
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public CleanAction Action { get; set; } = CleanAction.Archive;

    public int DaysOld { get; set; } = 7;

    public string? Instructions { get; set; }

    public bool? SkipReply { get; set; }

    public bool? SkipStarred { get; set; }

    public bool? SkipCalendar { get; set; }

    public bool? SkipReceipt { get; set; }

    public bool? SkipAttachment { get; set; }

    public bool? SkipConversation { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public virtual ICollection<CleanupThread> Threads { get; set; } = new List<CleanupThread>();
}
