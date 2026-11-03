#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum BulkActionType
{
    [PgName("CANCEL")]
    Cancel,

    [PgName("REPLAY")]
    Replay,
}
