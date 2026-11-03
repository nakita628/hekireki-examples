#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum BatchTaskRunStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("PROCESSING")]
    Processing,

    [PgName("COMPLETED")]
    Completed,

    [PgName("PARTIAL_FAILED")]
    PartialFailed,

    [PgName("ABORTED")]
    Aborted,
}
