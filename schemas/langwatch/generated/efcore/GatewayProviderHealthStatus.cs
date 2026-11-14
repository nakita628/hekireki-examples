#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum GatewayProviderHealthStatus
{
    [PgName("UNKNOWN")]
    Unknown,

    [PgName("HEALTHY")]
    Healthy,

    [PgName("DEGRADED")]
    Degraded,

    [PgName("CIRCUIT_OPEN")]
    CircuitOpen,
}
