#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum RetentionRunKind
{
    [PgName("sweep")]
    Sweep,

    [PgName("purge")]
    Purge,
}
