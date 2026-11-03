#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum SpaceMemberRole
{
    [PgName("ADMIN")]
    Admin,

    [PgName("MEMBER")]
    Member,
}
