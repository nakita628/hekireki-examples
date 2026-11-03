#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum TaskQueueVersion
{
    [PgName("V1")]
    V1,

    [PgName("V2")]
    V2,
}
