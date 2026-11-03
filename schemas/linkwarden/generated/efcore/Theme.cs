#nullable enable

using NpgsqlTypes;

namespace Linkwarden.Models;

public enum Theme
{
    [PgName("dark")]
    Dark,

    [PgName("light")]
    Light,

    [PgName("auto")]
    Auto,
}
