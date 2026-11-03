#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum Role
{
    [PgName("ADMIN")]
    Admin,

    [PgName("USER")]
    User,
}
