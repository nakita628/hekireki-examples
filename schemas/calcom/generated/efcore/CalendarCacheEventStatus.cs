#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum CalendarCacheEventStatus
{
    [PgName("confirmed")]
    Confirmed,

    [PgName("tentative")]
    Tentative,

    [PgName("cancelled")]
    Cancelled,
}
