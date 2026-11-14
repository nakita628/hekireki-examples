#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum RetentionPolicyScopeType
{
    [PgName("ORGANIZATION")]
    Organization,

    [PgName("TEAM")]
    Team,

    [PgName("PROJECT")]
    Project,
}
