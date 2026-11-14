#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum VirtualKeyRoutingMode
{
    [PgName("NONE")]
    None,

    [PgName("FALLBACK_ALL")]
    FallbackAll,

    [PgName("POLICY")]
    Policy,
}
