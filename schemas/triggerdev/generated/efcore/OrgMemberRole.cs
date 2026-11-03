#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum OrgMemberRole
{
    [PgName("ADMIN")]
    Admin,

    [PgName("MEMBER")]
    Member,
}
