#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum BookingReportStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("DISMISSED")]
    Dismissed,

    [PgName("BLOCKED")]
    Blocked,
}
