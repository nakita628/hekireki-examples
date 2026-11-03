#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum WrongAssignmentReportStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("REVIEWED")]
    Reviewed,

    [PgName("RESOLVED")]
    Resolved,

    [PgName("DISMISSED")]
    Dismissed,
}
