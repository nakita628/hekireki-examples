#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum JobConfigState
{
    [PgName("ACTIVE")]
    Active,

    [PgName("INACTIVE")]
    Inactive,
}
