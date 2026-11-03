#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum WatchlistAction
{
    [PgName("REPORT")]
    Report,

    [PgName("BLOCK")]
    Block,

    [PgName("ALERT")]
    Alert,
}
