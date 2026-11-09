#nullable enable

using System;

namespace Jitsu.Models;

public partial class SourceTask
{
    public string SyncId { get; set; } = null!;

    public string TaskId { get; set; } = null!;

    public string Package { get; set; } = null!;

    public string Version { get; set; } = null!;

    public DateTime StartedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? StartedBy { get; set; }

    public string Status { get; set; } = null!;

    public string? Description { get; set; }

    public string? Metrics { get; set; }

    public string? Error { get; set; }
}
