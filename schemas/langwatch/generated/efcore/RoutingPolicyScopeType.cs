#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum RoutingPolicyScopeType
{
    [PgName("ORGANIZATION")]
    Organization,

    [PgName("TEAM")]
    Team,

    [PgName("PROJECT")]
    Project,
}
