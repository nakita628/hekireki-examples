#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum ProjectAlertStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("SENT")]
    Sent,

    [PgName("FAILED")]
    Failed,
}
