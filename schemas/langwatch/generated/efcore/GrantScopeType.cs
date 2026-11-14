#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum GrantScopeType
{
    [PgName("ORGANIZATION")]
    Organization,

    [PgName("TEAM")]
    Team,

    [PgName("PROJECT")]
    Project,

    [PgName("RESOURCE")]
    Resource,

    [PgName("PLATFORM")]
    Platform,
}
