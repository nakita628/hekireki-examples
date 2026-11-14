#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum AlertType
{
    [PgName("CRITICAL")]
    Critical,

    [PgName("WARNING")]
    Warning,

    [PgName("INFO")]
    Info,
}
