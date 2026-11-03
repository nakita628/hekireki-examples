#nullable enable

using System;

namespace Teable.Models;

public partial class BaseDataDbMoveJob
{
    public string Id { get; set; } = null!;

    public string BaseId { get; set; } = null!;

    public string SourceSpaceId { get; set; } = null!;

    public string TargetSpaceId { get; set; } = null!;

    public string? SourceConnectionId { get; set; }

    public string? TargetConnectionId { get; set; }

    public BaseDataDbMoveJobState State { get; set; } = BaseDataDbMoveJobState.Pending;

    public string? Inventory { get; set; }

    public string? CopyStats { get; set; }

    public string? ValidationStats { get; set; }

    public string? LastError { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual Base Base { get; set; } = null!;

    public virtual Space SourceSpace { get; set; } = null!;

    public virtual Space TargetSpace { get; set; } = null!;
}
