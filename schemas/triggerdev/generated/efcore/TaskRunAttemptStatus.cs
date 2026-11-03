#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum TaskRunAttemptStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("EXECUTING")]
    Executing,

    [PgName("PAUSED")]
    Paused,

    [PgName("FAILED")]
    Failed,

    [PgName("CANCELED")]
    Canceled,

    [PgName("COMPLETED")]
    Completed,
}
