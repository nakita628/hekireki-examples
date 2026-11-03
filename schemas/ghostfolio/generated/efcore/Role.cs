#nullable enable

using NpgsqlTypes;

namespace Ghostfolio.Models;

public enum Role
{
    [PgName("ADMIN")]
    Admin,

    [PgName("DEMO")]
    Demo,

    [PgName("INACTIVE")]
    Inactive,

    [PgName("USER")]
    User,
}
