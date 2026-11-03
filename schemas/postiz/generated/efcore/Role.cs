#nullable enable

using NpgsqlTypes;

namespace Postiz.Models;

public enum Role
{
    [PgName("SUPERADMIN")]
    Superadmin,

    [PgName("ADMIN")]
    Admin,

    [PgName("USER")]
    User,
}
