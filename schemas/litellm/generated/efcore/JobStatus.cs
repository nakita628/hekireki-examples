#nullable enable

using NpgsqlTypes;

namespace LiteLLM.Models;

public enum JobStatus
{
    [PgName("ACTIVE")]
    Active,

    [PgName("INACTIVE")]
    Inactive,
}
