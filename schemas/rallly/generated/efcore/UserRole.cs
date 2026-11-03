#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum UserRole
{
    [PgName("admin")]
    Admin,

    [PgName("user")]
    User,
}
