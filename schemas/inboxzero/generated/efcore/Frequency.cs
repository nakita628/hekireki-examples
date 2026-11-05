#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum Frequency
{
    [PgName("NEVER")]
    Never,

    [PgName("DAILY")]
    Daily,

    [PgName("WEEKLY")]
    Weekly,
}
