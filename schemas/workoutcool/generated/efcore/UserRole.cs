#nullable enable

using NpgsqlTypes;

namespace WorkoutCool.Models;

public enum UserRole
{
    [PgName("admin")]
    Admin,

    [PgName("user")]
    User,
}
