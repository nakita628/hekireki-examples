#nullable enable

using NpgsqlTypes;

namespace WorkoutCool.Models;

public enum ExerciseAttributeNameEnum
{
    [PgName("TYPE")]
    Type,

    [PgName("PRIMARY_MUSCLE")]
    PrimaryMuscle,

    [PgName("SECONDARY_MUSCLE")]
    SecondaryMuscle,

    [PgName("EQUIPMENT")]
    Equipment,

    [PgName("MECHANICS_TYPE")]
    MechanicsType,
}
