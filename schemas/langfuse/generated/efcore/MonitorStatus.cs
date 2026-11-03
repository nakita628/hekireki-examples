#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum MonitorStatus
{
    [PgName("PAUSED")]
    Paused,

    [PgName("ACTIVE")]
    Active,

    [PgName("ERROR_BAD_QUERY")]
    ErrorBadQuery,
}
