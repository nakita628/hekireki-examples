#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class ScheduledJobRuns
{
    public int Id { get; set; }

    public int JobId { get; set; }

    public string Status { get; set; } = "queued";

    public string? Result { get; set; }

    public string? Error { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? ReadAt { get; set; }

    public virtual ScheduledJobs Job { get; set; } = null!;
}
