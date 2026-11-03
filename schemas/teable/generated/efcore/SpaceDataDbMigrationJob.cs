#nullable enable

using System;

namespace Teable.Models;

public partial class SpaceDataDbMigrationJob
{
    public string Id { get; set; } = null!;

    public string SpaceId { get; set; } = null!;

    public string? SourceConnectionId { get; set; }

    public string? TargetConnectionId { get; set; }

    public string TargetMode { get; set; } = "migrate-space";

    public bool SwitchOnCompletion { get; set; } = false;

    public SpaceDataDbMigrationJobState State { get; set; } = SpaceDataDbMigrationJobState.Pending;

    public string TargetUrlFingerprint { get; set; } = null!;

    public string TargetInternalSchema { get; set; } = null!;

    public string? Inventory { get; set; }

    public string? CopyStats { get; set; }

    public string? ValidationStats { get; set; }

    public string? LastError { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual Space Space { get; set; } = null!;

    public virtual DataDbConnection? SourceConnection { get; set; }

    public virtual DataDbConnection? TargetConnection { get; set; }
}
