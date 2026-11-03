#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum SystemReportStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("BLOCKED")]
    Blocked,

    [PgName("DISMISSED")]
    Dismissed,
}
