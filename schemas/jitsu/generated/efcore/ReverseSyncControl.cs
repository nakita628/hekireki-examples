#nullable enable

namespace Jitsu.Models;

public partial class ReverseSyncControl
{
    public string WorkspaceId { get; set; } = null!;

    public string SyncId { get; set; } = null!;

    public string RunId { get; set; } = null!;

    public long RunOrder { get; set; } = 0L;

    public bool Detached { get; set; } = false;

    public string Revision { get; set; } = null!;

    public string TargetHash { get; set; } = null!;

    public string Phase { get; set; } = "new";

    public ReverseSyncMode Mode { get; set; }

    public ReverseSyncExtraction Extraction { get; set; }

    public byte[]? ArtifactHead { get; set; }

    public long BaseSequence { get; set; } = 0L;

    public long NextSequence { get; set; } = 0L;

    public long CheckpointSequence { get; set; } = 0L;

    public long? FinishSequence { get; set; }

    public byte[]? Store { get; set; }

    public byte[]? ProviderState { get; set; }

    public byte[]? FinishResult { get; set; }

    public string? CommittedGeneration { get; set; }
}
