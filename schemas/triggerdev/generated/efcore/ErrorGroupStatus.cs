#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum ErrorGroupStatus
{
    [PgName("UNRESOLVED")]
    Unresolved,

    [PgName("RESOLVED")]
    Resolved,

    [PgName("IGNORED")]
    Ignored,
}
