#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum ExecutedRuleStatus
{
    [PgName("APPLIED")]
    Applied,

    [PgName("APPLYING")]
    Applying,

    [PgName("REJECTED")]
    Rejected,

    [PgName("PENDING")]
    Pending,

    [PgName("SKIPPED")]
    Skipped,

    [PgName("ERROR")]
    Error,
}
