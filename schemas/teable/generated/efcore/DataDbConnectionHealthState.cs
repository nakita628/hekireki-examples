#nullable enable

using NpgsqlTypes;

namespace Teable.Models;

public enum DataDbConnectionHealthState
{
    [PgName("healthy")]
    Healthy,

    [PgName("read_only")]
    ReadOnly,

    [PgName("unreachable")]
    Unreachable,

    [PgName("degraded")]
    Degraded,
}
