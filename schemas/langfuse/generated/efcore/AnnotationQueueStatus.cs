#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum AnnotationQueueStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("COMPLETED")]
    Completed,
}
