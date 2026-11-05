#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum SchedulingStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("SCHEDULED")]
    Scheduled,

    [PgName("FAILED")]
    Failed,
}
