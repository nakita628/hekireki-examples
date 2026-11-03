#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum SupportAccessRequestStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("APPROVED")]
    Approved,

    [PgName("CANCELLED")]
    Cancelled,
}
