#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum SnoozedThreadStatus
{
    [PgName("PREPARING")]
    Preparing,

    [PgName("PENDING")]
    Pending,

    [PgName("EXECUTING")]
    Executing,

    [PgName("COMPLETED")]
    Completed,

    [PgName("CANCELLED")]
    Cancelled,
}
