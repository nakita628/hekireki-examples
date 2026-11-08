#nullable enable

using NpgsqlTypes;

namespace Plunk.Models;

public enum WorkflowExecutionStatus
{
    [PgName("RUNNING")]
    Running,

    [PgName("WAITING")]
    Waiting,

    [PgName("COMPLETED")]
    Completed,

    [PgName("EXITED")]
    Exited,

    [PgName("FAILED")]
    Failed,

    [PgName("CANCELLED")]
    Cancelled,
}
