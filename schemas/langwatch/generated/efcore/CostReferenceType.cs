#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum CostReferenceType
{
    [PgName("CHECK")]
    Check,

    [PgName("TRACE")]
    Trace,

    [PgName("PROJECT")]
    Project,

    [PgName("BATCH")]
    Batch,
}
