#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum RetentionRunItemAction
{
    [PgName("notified")]
    Notified,

    [PgName("archived")]
    Archived,

    [PgName("deactivated")]
    Deactivated,

    [PgName("deleted")]
    Deleted,

    [PgName("skipped")]
    Skipped,
}
