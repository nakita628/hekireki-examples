#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum BookingStatus
{
    [PgName("PENDING_PROVIDER_EVENT")]
    PendingProviderEvent,

    [PgName("CONFIRMED")]
    Confirmed,

    [PgName("CANCELED")]
    Canceled,

    [PgName("FAILED")]
    Failed,
}
