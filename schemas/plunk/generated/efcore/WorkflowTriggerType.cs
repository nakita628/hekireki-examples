#nullable enable

using NpgsqlTypes;

namespace Plunk.Models;

public enum WorkflowTriggerType
{
    [PgName("EVENT")]
    Event,

    [PgName("MANUAL")]
    Manual,

    [PgName("SCHEDULE")]
    Schedule,
}
