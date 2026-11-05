#nullable enable

using NpgsqlTypes;

namespace OpenPanel.Models;

public enum Interval
{
    [PgName("hour")]
    Hour,

    [PgName("day")]
    Day,

    [PgName("month")]
    Month,

    [PgName("minute")]
    Minute,

    [PgName("week")]
    Week,
}
