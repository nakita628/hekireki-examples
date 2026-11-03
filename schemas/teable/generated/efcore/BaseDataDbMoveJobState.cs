#nullable enable

using NpgsqlTypes;

namespace Teable.Models;

public enum BaseDataDbMoveJobState
{
    [PgName("pending")]
    Pending,

    [PgName("waiting_worker")]
    WaitingWorker,

    [PgName("copying_base_schema")]
    CopyingBaseSchema,

    [PgName("copying_shared_rows")]
    CopyingSharedRows,

    [PgName("validating")]
    Validating,

    [PgName("switching")]
    Switching,

    [PgName("succeeded")]
    Succeeded,

    [PgName("failed")]
    Failed,

    [PgName("cancelled")]
    Cancelled,
}
