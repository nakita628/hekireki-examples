#nullable enable

using System.Collections.Generic;

namespace SuperProductivity.Models;

public partial class Operation
{
    public string Id { get; set; } = null!;

    public int UserId { get; set; }

    public string ClientId { get; set; } = null!;

    public int ServerSeq { get; set; }

    public string ActionType { get; set; } = null!;

    public string OpType { get; set; } = null!;

    public string EntityType { get; set; } = null!;

    public string? EntityId { get; set; }

    public List<string>? EntityIds { get; set; } = new List<string>();

    public string Payload { get; set; } = null!;

    public long PayloadBytes { get; set; } = 0L;

    public string VectorClock { get; set; } = null!;

    public int SchemaVersion { get; set; }

    public long ClientTimestamp { get; set; }

    public long ReceivedAt { get; set; }

    public bool IsPayloadEncrypted { get; set; } = false;

    public string? SyncImportReason { get; set; }

    public int? RepairBaseServerSeq { get; set; }

    public virtual User User { get; set; } = null!;
}
