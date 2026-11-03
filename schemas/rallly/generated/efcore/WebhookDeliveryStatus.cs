#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum WebhookDeliveryStatus
{
    [PgName("pending")]
    Pending,

    [PgName("in_flight")]
    InFlight,

    [PgName("succeeded")]
    Succeeded,

    [PgName("failed")]
    Failed,

    [PgName("exhausted")]
    Exhausted,
}
