#nullable enable

using NpgsqlTypes;

namespace Ghostfolio.Models;

public enum MarketDataState
{
    [PgName("CLOSE")]
    Close,

    [PgName("INTRADAY")]
    Intraday,
}
