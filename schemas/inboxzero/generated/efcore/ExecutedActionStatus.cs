#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum ExecutedActionStatus
{
    [PgName("SUCCEEDED")]
    Succeeded,

    [PgName("FAILED")]
    Failed,

    [PgName("SKIPPED")]
    Skipped,
}
