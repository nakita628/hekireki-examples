#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum GatewayConnectionStatus
{
    [PgName("enabled")]
    Enabled,

    [PgName("disabled")]
    Disabled,

    [PgName("error")]
    Error,
}
