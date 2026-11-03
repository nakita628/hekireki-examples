#nullable enable

using NpgsqlTypes;

namespace Ghostfolio.Models;

public enum AssetSubClass
{
    [PgName("BOND")]
    Bond,

    [PgName("CASH")]
    Cash,

    [PgName("COLLECTIBLE")]
    Collectible,

    [PgName("COMMODITY")]
    Commodity,

    [PgName("CRYPTOCURRENCY")]
    Cryptocurrency,

    [PgName("ETF")]
    Etf,

    [PgName("LOAN")]
    Loan,

    [PgName("MUTUALFUND")]
    Mutualfund,

    [PgName("PRECIOUS_METAL")]
    PreciousMetal,

    [PgName("PRIVATE_EQUITY")]
    PrivateEquity,

    [PgName("STOCK")]
    Stock,
}
