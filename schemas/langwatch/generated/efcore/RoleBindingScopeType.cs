#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum RoleBindingScopeType
{
    [PgName("ORGANIZATION")]
    Organization,

    [PgName("TEAM")]
    Team,

    [PgName("PROJECT")]
    Project,
}
