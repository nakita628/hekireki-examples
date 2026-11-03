#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum WaitpointStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("COMPLETED")]
    Completed,
}
