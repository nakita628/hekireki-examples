#nullable enable

using System;

namespace InboxZero.Models;

public partial class AutomationJobRun
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public AutomationJobRunStatus Status { get; set; } = AutomationJobRunStatus.Pending;

    public DateTime ScheduledFor { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public string? OutboundMessage { get; set; }

    public string? ProviderMessageId { get; set; }

    public string? Error { get; set; }

    public string AutomationJobId { get; set; } = null!;

    public virtual AutomationJob AutomationJob { get; set; } = null!;
}
