#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum BulkActionStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("COMPLETED")]
    Completed,

    [PgName("ABORTED")]
    Aborted,
}
