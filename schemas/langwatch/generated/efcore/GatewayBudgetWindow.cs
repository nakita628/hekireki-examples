#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum GatewayBudgetWindow
{
    [PgName("MINUTE")]
    Minute,

    [PgName("HOUR")]
    Hour,

    [PgName("DAY")]
    Day,

    [PgName("WEEK")]
    Week,

    [PgName("MONTH")]
    Month,

    [PgName("TOTAL")]
    Total,

    [PgName("MANUAL")]
    Manual,
}
