#nullable enable

using NpgsqlTypes;

namespace Plunk.Models;

public enum StepExecutionStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("SCHEDULED")]
    Scheduled,

    [PgName("WAITING")]
    Waiting,

    [PgName("RUNNING")]
    Running,

    [PgName("COMPLETED")]
    Completed,

    [PgName("SKIPPED")]
    Skipped,

    [PgName("FAILED")]
    Failed,
}
