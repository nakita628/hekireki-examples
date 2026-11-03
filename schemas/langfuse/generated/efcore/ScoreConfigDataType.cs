#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum ScoreConfigDataType
{
    [PgName("CATEGORICAL")]
    Categorical,

    [PgName("NUMERIC")]
    Numeric,

    [PgName("BOOLEAN")]
    Boolean,

    [PgName("TEXT")]
    Text,
}
