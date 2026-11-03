#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum WorkflowRunLogStatus
{
    [PgName("pending")]
    Pending,

    [PgName("running")]
    Running,

    [PgName("succeeded")]
    Succeeded,

    [PgName("failed")]
    Failed,

    [PgName("skipped")]
    Skipped,
}
