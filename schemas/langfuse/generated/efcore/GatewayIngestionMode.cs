#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum GatewayIngestionMode
{
    [PgName("none")]
    None,

    [PgName("usage")]
    Usage,

    [PgName("full")]
    Full,
}
