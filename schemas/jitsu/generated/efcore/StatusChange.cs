#nullable enable

using System;

namespace Jitsu.Models;

public partial class StatusChange
{
    public long Id { get; set; }

    public string WorkspaceId { get; set; } = null!;

    public string ActorId { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public string Status { get; set; } = null!;

    public DateTime StartedAt { get; set; }

    public int Counts { get; set; } = 0;

    public string? Description { get; set; }

    public int QueueSize { get; set; } = 0;
}
