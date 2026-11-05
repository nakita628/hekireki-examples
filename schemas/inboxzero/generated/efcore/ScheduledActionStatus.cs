#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum ScheduledActionStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("EXECUTING")]
    Executing,

    [PgName("COMPLETED")]
    Completed,

    [PgName("FAILED")]
    Failed,

    [PgName("CANCELLED")]
    Cancelled,
}
