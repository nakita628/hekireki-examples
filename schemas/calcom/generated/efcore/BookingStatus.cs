#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum BookingStatus
{
    [PgName("cancelled")]
    Cancelled,

    [PgName("accepted")]
    Accepted,

    [PgName("rejected")]
    Rejected,

    [PgName("pending")]
    Pending,

    [PgName("awaiting_host")]
    AwaitingHost,
}
