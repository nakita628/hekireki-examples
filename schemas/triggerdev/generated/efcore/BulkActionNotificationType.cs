#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum BulkActionNotificationType
{
    [PgName("NONE")]
    None,

    [PgName("EMAIL")]
    Email,
}
