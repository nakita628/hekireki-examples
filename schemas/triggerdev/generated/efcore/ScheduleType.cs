#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum ScheduleType
{
    [PgName("DECLARATIVE")]
    Declarative,

    [PgName("IMPERATIVE")]
    Imperative,
}
