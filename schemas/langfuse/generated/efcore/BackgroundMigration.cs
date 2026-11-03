#nullable enable

using System;

namespace Langfuse.Models;

public partial class BackgroundMigration
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Script { get; set; } = null!;

    public string Args { get; set; } = null!;

    public string State { get; set; } = "{}";

    public DateTime? FinishedAt { get; set; }

    public DateTime? FailedAt { get; set; }

    public string? FailedReason { get; set; }

    public string? WorkerId { get; set; }

    public DateTime? LockedAt { get; set; }
}
