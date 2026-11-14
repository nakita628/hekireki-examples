#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum OrganizationUserRole
{
    [PgName("ADMIN")]
    Admin,

    [PgName("MEMBER")]
    Member,

    [PgName("EXTERNAL")]
    External,

    [PgName("DEVELOPER")]
    Developer,
}
