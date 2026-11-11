#nullable enable

using System;

namespace Trigger.Models;

public partial class WorkerGroupToken
{
    public string Id { get; set; } = null!;

    public string TokenHash { get; set; } = null!;

    public string? PreviousTokenHash { get; set; }

    public DateTime? PreviousTokenValidUntil { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual WorkerInstanceGroup? WorkerGroup { get; set; }
}
