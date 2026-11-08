#nullable enable

using NpgsqlTypes;

namespace WorkoutCool.Models;

public enum ExercisePrivacy
{
    [PgName("PUBLIC")]
    Public,

    [PgName("PRIVATE")]
    Private,
}
