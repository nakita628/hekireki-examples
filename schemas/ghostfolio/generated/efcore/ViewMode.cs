#nullable enable

using NpgsqlTypes;

namespace Ghostfolio.Models;

public enum ViewMode
{
    [PgName("DEFAULT")]
    Default,

    [PgName("ZEN")]
    Zen,
}
