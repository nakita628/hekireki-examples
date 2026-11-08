#nullable enable

using NpgsqlTypes;

namespace WorkoutCool.Models;

public enum Platform
{
    [PgName("WEB")]
    Web,

    [PgName("IOS")]
    Ios,

    [PgName("ANDROID")]
    Android,
}
