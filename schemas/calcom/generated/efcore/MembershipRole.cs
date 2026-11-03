#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum MembershipRole
{
    [PgName("MEMBER")]
    Member,

    [PgName("ADMIN")]
    Admin,

    [PgName("OWNER")]
    Owner,
}
