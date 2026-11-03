#nullable enable

using NpgsqlTypes;

namespace Ghostfolio.Models;

public enum DataSource
{
    [PgName("ALPHA_VANTAGE")]
    AlphaVantage,

    [PgName("COINGECKO")]
    Coingecko,

    [PgName("EOD_HISTORICAL_DATA")]
    EodHistoricalData,

    [PgName("FINANCIAL_MODELING_PREP")]
    FinancialModelingPrep,

    [PgName("GHOSTFOLIO")]
    Ghostfolio,

    [PgName("GOOGLE_SHEETS")]
    GoogleSheets,

    [PgName("MANUAL")]
    Manual,

    [PgName("RAPID_API")]
    RapidApi,

    [PgName("YAHOO")]
    Yahoo,
}
