#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum GatewayProviderRotationPolicy
{
    [PgName("MANUAL")]
    Manual,
}
