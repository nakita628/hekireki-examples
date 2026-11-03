#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum ExecutionStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("IN_PROGRESS")]
    InProgress,

    [PgName("COMPLETED")]
    Completed,

    [PgName("FAILED")]
    Failed,

    [PgName("BLOCKED")]
    Blocked,
}
