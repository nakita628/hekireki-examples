#nullable enable

using NpgsqlTypes;

namespace WorkoutCool.Models;

public enum ProgramVisibility
{
    [PgName("DRAFT")]
    Draft,

    [PgName("PUBLISHED")]
    Published,

    [PgName("ARCHIVED")]
    Archived,
}
