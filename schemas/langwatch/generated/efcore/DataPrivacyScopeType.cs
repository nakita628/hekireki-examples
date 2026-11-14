#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum DataPrivacyScopeType
{
    [PgName("ORGANIZATION")]
    Organization,

    [PgName("DEPARTMENT")]
    Department,

    [PgName("TEAM")]
    Team,

    [PgName("PROJECT")]
    Project,
}
