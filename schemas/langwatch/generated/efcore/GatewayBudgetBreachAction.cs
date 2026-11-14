#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum GatewayBudgetBreachAction
{
    [PgName("BLOCK")]
    Block,

    [PgName("WARN")]
    Warn,
}
