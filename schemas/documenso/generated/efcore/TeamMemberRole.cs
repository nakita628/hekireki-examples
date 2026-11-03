#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum TeamMemberRole
{
    [PgName("ADMIN")]
    Admin,

    [PgName("MANAGER")]
    Manager,

    [PgName("MEMBER")]
    Member,
}
