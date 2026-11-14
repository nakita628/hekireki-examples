#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum ModelProviderScopeType
{
    [PgName("ORGANIZATION")]
    Organization,

    [PgName("TEAM")]
    Team,

    [PgName("PROJECT")]
    Project,
}
