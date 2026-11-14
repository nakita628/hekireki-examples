#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum PlanTypes
{
    [PgName("FREE")]
    Free,

    [PgName("PRO")]
    Pro,

    [PgName("GROWTH")]
    Growth,

    [PgName("GROWTH_SEAT_EUR_MONTHLY")]
    GrowthSeatEurMonthly,

    [PgName("GROWTH_SEAT_EUR_ANNUAL")]
    GrowthSeatEurAnnual,

    [PgName("GROWTH_SEAT_USD_MONTHLY")]
    GrowthSeatUsdMonthly,

    [PgName("GROWTH_SEAT_USD_ANNUAL")]
    GrowthSeatUsdAnnual,

    [PgName("ENTERPRISE")]
    Enterprise,

    [PgName("LAUNCH")]
    Launch,

    [PgName("ACCELERATE")]
    Accelerate,

    [PgName("LAUNCH_ANNUAL")]
    LaunchAnnual,

    [PgName("ACCELERATE_ANNUAL")]
    AccelerateAnnual,
}
