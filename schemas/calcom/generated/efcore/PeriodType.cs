#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum PeriodType
{
    [PgName("unlimited")]
    Unlimited,

    [PgName("rolling")]
    Rolling,

    [PgName("rolling_window")]
    RollingWindow,

    [PgName("range")]
    Range,
}
