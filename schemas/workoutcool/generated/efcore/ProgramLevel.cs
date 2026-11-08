#nullable enable

using NpgsqlTypes;

namespace WorkoutCool.Models;

public enum ProgramLevel
{
    [PgName("BEGINNER")]
    Beginner,

    [PgName("INTERMEDIATE")]
    Intermediate,

    [PgName("ADVANCED")]
    Advanced,

    [PgName("EXPERT")]
    Expert,
}
