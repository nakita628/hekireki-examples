#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum RRResetInterval
{
    [PgName("MONTH")]
    Month,

    [PgName("DAY")]
    Day,
}
