#nullable enable

using NpgsqlTypes;

namespace Plunk.Models;

public enum Role
{
    [PgName("OWNER")]
    Owner,

    [PgName("ADMIN")]
    Admin,

    [PgName("MEMBER")]
    Member,
}
