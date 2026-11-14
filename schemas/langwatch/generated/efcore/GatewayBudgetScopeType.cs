#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum GatewayBudgetScopeType
{
    [PgName("ORGANIZATION")]
    Organization,

    [PgName("TEAM")]
    Team,

    [PgName("PROJECT")]
    Project,

    [PgName("VIRTUAL_KEY")]
    VirtualKey,

    [PgName("PRINCIPAL")]
    Principal,

    [PgName("GROUP")]
    Group,

    [PgName("ATTRIBUTED_USER")]
    AttributedUser,
}
