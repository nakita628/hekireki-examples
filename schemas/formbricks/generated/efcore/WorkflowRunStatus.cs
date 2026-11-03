#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum WorkflowRunStatus
{
    [PgName("queued")]
    Queued,

    [PgName("running")]
    Running,

    [PgName("completed")]
    Completed,

    [PgName("failed")]
    Failed,

    [PgName("canceled")]
    Canceled,
}
