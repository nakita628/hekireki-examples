#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum TaskEventLevel
{
    [PgName("TRACE")]
    Trace,

    [PgName("DEBUG")]
    Debug,

    [PgName("LOG")]
    Log,

    [PgName("INFO")]
    Info,

    [PgName("WARN")]
    Warn,

    [PgName("ERROR")]
    Error,
}
