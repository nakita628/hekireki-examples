#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum MonitorSeverity
{
    [PgName("PAUSED")]
    Paused,

    [PgName("UNKNOWN")]
    Unknown,

    [PgName("NO_DATA")]
    NoData,

    [PgName("OK")]
    Ok,

    [PgName("WARNING")]
    Warning,

    [PgName("ALERT")]
    Alert,
}
