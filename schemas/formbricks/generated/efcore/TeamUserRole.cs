#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum TeamUserRole
{
    [PgName("admin")]
    Admin,

    [PgName("contributor")]
    Contributor,
}
