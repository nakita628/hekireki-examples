#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum BulkInboxActionJobStatus
{
    [PgName("RUNNING")]
    Running,

    [PgName("COMPLETED")]
    Completed,

    [PgName("FAILED")]
    Failed,

    [PgName("CANCELLED")]
    Cancelled,
}
