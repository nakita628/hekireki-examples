#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMScheduledJobRun
{
    public string Id { get; set; } = null!;

    public string JobName { get; set; } = null!;

    public string LockGroup { get; set; } = null!;

    public string Schedule { get; set; } = null!;

    public DateTime ScheduledFor { get; set; }

    public ScheduledJobRunStatus Status { get; set; } = ScheduledJobRunStatus.Pending;

    public string? ClaimedBy { get; set; }

    public DateTime? ClaimedAt { get; set; }

    public DateTime? LeaseExpiresAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
