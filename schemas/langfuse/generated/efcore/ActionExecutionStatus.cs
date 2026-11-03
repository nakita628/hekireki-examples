#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum ActionExecutionStatus
{
    [PgName("COMPLETED")]
    Completed,

    [PgName("ERROR")]
    Error,

    [PgName("PENDING")]
    Pending,

    [PgName("CANCELLED")]
    Cancelled,
}
