#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum UserPermissionRole
{
    [PgName("USER")]
    User,

    [PgName("ADMIN")]
    Admin,
}
