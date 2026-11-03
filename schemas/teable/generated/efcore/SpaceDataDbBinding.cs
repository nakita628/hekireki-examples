#nullable enable

using System;

namespace Teable.Models;

public partial class SpaceDataDbBinding
{
    public string Id { get; set; } = null!;

    public string SpaceId { get; set; } = null!;

    public string? DataDbConnectionId { get; set; }

    public SpaceDataDbBindingMode Mode { get; set; } = SpaceDataDbBindingMode.Default;

    public SpaceDataDbBindingState State { get; set; } = SpaceDataDbBindingState.Ready;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime? LastHistoryFlushedAt { get; set; }

    public DateTime? LastRemovalFlushedAt { get; set; }

    public virtual Space Space { get; set; } = null!;

    public virtual DataDbConnection? DataDbConnection { get; set; }
}
