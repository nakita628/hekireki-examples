#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum QueuedEmailStatus
{
    [PgName("pending")]
    Pending,

    [PgName("sent")]
    Sent,

    [PgName("skipped")]
    Skipped,

    [PgName("failed")]
    Failed,
}
