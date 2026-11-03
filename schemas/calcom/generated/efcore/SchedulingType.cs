#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum SchedulingType
{
    [PgName("roundRobin")]
    RoundRobin,

    [PgName("collective")]
    Collective,

    [PgName("managed")]
    Managed,
}
