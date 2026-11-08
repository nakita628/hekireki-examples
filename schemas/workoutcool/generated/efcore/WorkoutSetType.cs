#nullable enable

using NpgsqlTypes;

namespace WorkoutCool.Models;

public enum WorkoutSetType
{
    [PgName("TIME")]
    Time,

    [PgName("WEIGHT")]
    Weight,

    [PgName("REPS")]
    Reps,

    [PgName("BODYWEIGHT")]
    Bodyweight,

    [PgName("NA")]
    Na,
}
