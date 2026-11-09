#nullable enable

namespace Jitsu.Models;

public partial class ReverseSyncTargetOwner
{
    public string TargetHash { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public string SyncId { get; set; } = null!;
}
