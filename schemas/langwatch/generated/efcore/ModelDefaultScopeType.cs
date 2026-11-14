#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum ModelDefaultScopeType
{
    [PgName("ORGANIZATION")]
    Organization,

    [PgName("TEAM")]
    Team,

    [PgName("PROJECT")]
    Project,
}
