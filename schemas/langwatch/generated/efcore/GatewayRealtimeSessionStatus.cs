#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum GatewayRealtimeSessionStatus
{
    [PgName("OPEN")]
    Open,

    [PgName("CLOSED")]
    Closed,

    [PgName("FAILED")]
    Failed,

    [PgName("EXPIRED")]
    Expired,
}
