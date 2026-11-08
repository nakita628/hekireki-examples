#nullable enable

using NpgsqlTypes;

namespace WorkoutCool.Models;

public enum WorkoutSetUnit
{
    [PgName("kg")]
    Kg,

    [PgName("lbs")]
    Lbs,
}
