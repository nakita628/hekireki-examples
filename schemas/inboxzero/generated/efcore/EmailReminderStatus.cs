#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum EmailReminderStatus
{
    [PgName("NONE")]
    None,

    [PgName("PENDING")]
    Pending,

    [PgName("PROCESSING")]
    Processing,

    [PgName("COMPLETED")]
    Completed,

    [PgName("CANCELLED")]
    Cancelled,
}
