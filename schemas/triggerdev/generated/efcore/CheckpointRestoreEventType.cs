#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum CheckpointRestoreEventType
{
    [PgName("CHECKPOINT")]
    Checkpoint,

    [PgName("RESTORE")]
    Restore,
}
