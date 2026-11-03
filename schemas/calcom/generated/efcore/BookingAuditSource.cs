#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum BookingAuditSource
{
    [PgName("api_v1")]
    ApiV1,

    [PgName("api_v2")]
    ApiV2,

    [PgName("webapp")]
    Webapp,

    [PgName("webhook")]
    Webhook,

    [PgName("system")]
    System,

    [PgName("magic_link")]
    MagicLink,

    [PgName("unknown")]
    Unknown,
}
