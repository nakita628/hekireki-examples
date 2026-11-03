#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum TaskEventStatus
{
    [PgName("UNSET")]
    Unset,

    [PgName("OK")]
    Ok,

    [PgName("ERROR")]
    Error,

    [PgName("UNRECOGNIZED")]
    Unrecognized,
}
