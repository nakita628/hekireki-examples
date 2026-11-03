#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum NonprofitApplicationStatus
{
    [PgName("approved")]
    Approved,

    [PgName("rejected")]
    Rejected,

    [PgName("failed")]
    Failed,
}
