#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum BatchTaskRunItemStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("FAILED")]
    Failed,

    [PgName("CANCELED")]
    Canceled,

    [PgName("COMPLETED")]
    Completed,
}
