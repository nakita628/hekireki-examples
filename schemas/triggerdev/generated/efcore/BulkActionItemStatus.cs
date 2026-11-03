#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum BulkActionItemStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("COMPLETED")]
    Completed,

    [PgName("FAILED")]
    Failed,
}
