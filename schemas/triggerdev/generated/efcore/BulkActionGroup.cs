#nullable enable

using System;

namespace Trigger.Models;

public partial class BulkActionGroup
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual RuntimeEnvironment? Environment { get; set; }

    public string? EnvironmentId { get; set; }

    public BulkActionType Type { get; set; }

    public BulkActionStatus Status { get; set; } = BulkActionStatus.Pending;

    public string? QueryName { get; set; }

    public string? Params { get; set; }

    public string? DedupeKey { get; set; }

    public string? Cursor { get; set; }

    public int SuccessCount { get; set; } = 0;

    public int FailureCount { get; set; } = 0;

    public int TotalCount { get; set; } = 0;

    public BulkActionNotificationType CompletionNotification { get; set; } = BulkActionNotificationType.None;

    public virtual User? User { get; set; }

    public string? UserId { get; set; }

    public string? Name { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
