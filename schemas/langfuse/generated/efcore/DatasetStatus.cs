#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum DatasetStatus
{
    [PgName("ACTIVE")]
    Active,

    [PgName("ARCHIVED")]
    Archived,
}
