#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum BackgroundJobTaskStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("COMPLETED")]
    Completed,

    [PgName("FAILED")]
    Failed,
}
