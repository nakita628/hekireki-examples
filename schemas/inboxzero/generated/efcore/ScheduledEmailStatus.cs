#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum ScheduledEmailStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("PROCESSING")]
    Processing,

    [PgName("SENT")]
    Sent,

    [PgName("BLOCKED_AUTH")]
    BlockedAuth,

    [PgName("UNCERTAIN")]
    Uncertain,

    [PgName("FAILED")]
    Failed,

    [PgName("CANCELLED")]
    Cancelled,
}
