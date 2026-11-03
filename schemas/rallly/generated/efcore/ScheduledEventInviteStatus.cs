#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum ScheduledEventInviteStatus
{
    [PgName("pending")]
    Pending,

    [PgName("accepted")]
    Accepted,

    [PgName("declined")]
    Declined,

    [PgName("tentative")]
    Tentative,
}
