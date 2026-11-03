#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum WaitpointType
{
    [PgName("RUN")]
    Run,

    [PgName("DATETIME")]
    Datetime,

    [PgName("MANUAL")]
    Manual,

    [PgName("BATCH")]
    Batch,
}
