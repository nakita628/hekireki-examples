#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum TeamUserRole
{
    [PgName("ADMIN")]
    Admin,

    [PgName("MEMBER")]
    Member,

    [PgName("VIEWER")]
    Viewer,

    [PgName("CUSTOM")]
    Custom,
}
