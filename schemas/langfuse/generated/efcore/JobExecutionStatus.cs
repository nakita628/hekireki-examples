#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum JobExecutionStatus
{
    [PgName("COMPLETED")]
    Completed,

    [PgName("ERROR")]
    Error,

    [PgName("PENDING")]
    Pending,

    [PgName("CANCELLED")]
    Cancelled,

    [PgName("DELAYED")]
    Delayed,
}
