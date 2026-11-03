#nullable enable

using System;

namespace Trigger.Models;

public partial class BulkActionItem
{
    public string Id { get; set; } = null!;

    public string? FriendlyId { get; set; }

    public string GroupId { get; set; } = null!;

    public BulkActionType Type { get; set; }

    public BulkActionItemStatus Status { get; set; } = BulkActionItemStatus.Pending;

    public virtual TaskRun SourceRun { get; set; } = null!;

    public string SourceRunId { get; set; } = null!;

    public virtual TaskRun? DestinationRun { get; set; }

    public string? DestinationRunId { get; set; }

    public string? Error { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
