#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraBackgroundTasks
{
    public string Id { get; set; } = null!;

    public string ToolCallId { get; set; } = null!;

    public string ToolName { get; set; } = null!;

    public string AgentId { get; set; } = null!;

    public string RunId { get; set; } = null!;

    public string? ThreadId { get; set; }

    public string? ResourceId { get; set; }

    public string Status { get; set; } = null!;

    public string Args { get; set; } = null!;

    public string? Result { get; set; }

    public string? Error { get; set; }

    public string? SuspendPayload { get; set; }

    public int RetryCount { get; set; }

    public int MaxRetries { get; set; }

    public int TimeoutMs { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? SuspendedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? StartedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? SuspendedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAtZ { get; set; } = DateTime.UtcNow;
}
