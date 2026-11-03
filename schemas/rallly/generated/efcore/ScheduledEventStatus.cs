#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum ScheduledEventStatus
{
    [PgName("confirmed")]
    Confirmed,

    [PgName("canceled")]
    Canceled,

    [PgName("unconfirmed")]
    Unconfirmed,
}
