#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum Currency
{
    [PgName("USD")]
    Usd,

    [PgName("EUR")]
    Eur,
}
