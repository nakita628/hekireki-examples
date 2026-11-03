#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum BackgroundJobStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("PROCESSING")]
    Processing,

    [PgName("COMPLETED")]
    Completed,

    [PgName("FAILED")]
    Failed,
}
