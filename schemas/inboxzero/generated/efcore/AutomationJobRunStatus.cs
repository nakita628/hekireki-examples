#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum AutomationJobRunStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("RUNNING")]
    Running,

    [PgName("SENT")]
    Sent,

    [PgName("SKIPPED")]
    Skipped,

    [PgName("FAILED")]
    Failed,
}
