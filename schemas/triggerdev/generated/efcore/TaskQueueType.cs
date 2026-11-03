#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum TaskQueueType
{
    [PgName("VIRTUAL")]
    Virtual,

    [PgName("NAMED")]
    Named,
}
