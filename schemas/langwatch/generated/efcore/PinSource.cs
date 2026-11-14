#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum PinSource
{
    [PgName("manual")]
    Manual,

    [PgName("share")]
    Share,
}
