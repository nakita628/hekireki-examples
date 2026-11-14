#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum VirtualKeyScopeType
{
    [PgName("ORGANIZATION")]
    Organization,

    [PgName("TEAM")]
    Team,

    [PgName("PROJECT")]
    Project,
}
