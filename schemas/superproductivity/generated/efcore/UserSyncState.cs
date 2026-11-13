#nullable enable

namespace SuperProductivity.Models;

public partial class UserSyncState
{
    public int UserId { get; set; }

    public int LastSeq { get; set; } = 0;

    public int? LastSnapshotSeq { get; set; }

    public byte[]? SnapshotData { get; set; }

    public long? SnapshotAt { get; set; }

    public int? SnapshotSchemaVersion { get; set; } = 1;

    public int? LatestFullStateSeq { get; set; }

    public string? LatestFullStateVectorClock { get; set; }

    public int? LatestStateReplacementSeq { get; set; }

    public virtual User User { get; set; } = null!;
}
