#nullable enable

using System;

namespace InboxZero.Models;

public partial class SnoozedThread
{
    public string Id { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string ThreadId { get; set; } = null!;

    public DateTime ScheduledFor { get; set; }

    public SnoozedThreadStatus Status { get; set; } = SnoozedThreadStatus.Pending;

    public string? ClientMutationId { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
