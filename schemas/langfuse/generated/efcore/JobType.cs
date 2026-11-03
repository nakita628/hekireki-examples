#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum JobType
{
    [PgName("EVAL")]
    Eval,
}
