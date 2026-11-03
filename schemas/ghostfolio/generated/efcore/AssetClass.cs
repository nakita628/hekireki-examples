#nullable enable

using NpgsqlTypes;

namespace Ghostfolio.Models;

public enum AssetClass
{
    [PgName("ALTERNATIVE_INVESTMENT")]
    AlternativeInvestment,

    [PgName("COMMODITY")]
    Commodity,

    [PgName("EQUITY")]
    Equity,

    [PgName("FIXED_INCOME")]
    FixedIncome,

    [PgName("LIQUIDITY")]
    Liquidity,

    [PgName("REAL_ESTATE")]
    RealEstate,
}
