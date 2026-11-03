#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum TaskQueueRole
{
    [PgName("QUEUE")]
    Queue,

    [PgName("LIMIT")]
    Limit,
}
