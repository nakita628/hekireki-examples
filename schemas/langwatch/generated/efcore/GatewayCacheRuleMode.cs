#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum GatewayCacheRuleMode
{
    [PgName("RESPECT")]
    Respect,

    [PgName("FORCE")]
    Force,

    [PgName("DISABLE")]
    Disable,
}
