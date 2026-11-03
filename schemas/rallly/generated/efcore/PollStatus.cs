#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum PollStatus
{
    [PgName("open")]
    Open,

    [PgName("closed")]
    Closed,

    [PgName("scheduled")]
    Scheduled,

    [PgName("canceled")]
    Canceled,
}
