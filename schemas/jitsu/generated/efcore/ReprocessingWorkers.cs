#nullable enable

using System;

namespace Jitsu.Models;

public partial class ReprocessingWorkers
{
    public string JobId { get; set; } = null!;

    public int WorkerIndex { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? StartedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? CurrentFile { get; set; }

    public long? CurrentLine { get; set; } = 0L;

    public string? CurrentTs { get; set; }

    public int? AssignedFiles { get; set; } = 0;

    public int? ProcessedFiles { get; set; } = 0;

    public long? TotalLines { get; set; } = 0L;

    public long? SuccessCount { get; set; } = 0L;

    public long? ErrorCount { get; set; } = 0L;

    public long? SkippedCount { get; set; } = 0L;

    public long? TotalBytes { get; set; } = 0L;

    public long? ProcessedBytes { get; set; } = 0L;

    public string? Error { get; set; }

    public virtual ReprocessingJobs Job { get; set; } = null!;
}
