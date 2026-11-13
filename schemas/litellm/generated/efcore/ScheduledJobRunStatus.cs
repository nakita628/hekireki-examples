#nullable enable

using NpgsqlTypes;

namespace LiteLLM.Models;

public enum ScheduledJobRunStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("RUNNING")]
    Running,

    [PgName("SUCCEEDED")]
    Succeeded,

    [PgName("FAILED")]
    Failed,

    [PgName("CANCELLED")]
    Cancelled,
}
