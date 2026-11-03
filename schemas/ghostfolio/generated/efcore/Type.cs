#nullable enable

using NpgsqlTypes;

namespace Ghostfolio.Models;

public enum Type
{
    [PgName("BUY")]
    Buy,

    [PgName("DIVIDEND")]
    Dividend,

    [PgName("FEE")]
    Fee,

    [PgName("INTEREST")]
    Interest,

    [PgName("LIABILITY")]
    Liability,

    [PgName("SELL")]
    Sell,
}
