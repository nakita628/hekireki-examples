#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum TimeUnit
{
    [PgName("day")]
    Day,

    [PgName("hour")]
    Hour,

    [PgName("minute")]
    Minute,
}
