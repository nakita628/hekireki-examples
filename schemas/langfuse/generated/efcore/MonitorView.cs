#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum MonitorView
{
    [PgName("OBSERVATIONS")]
    Observations,

    [PgName("SCORES_NUMERIC")]
    ScoresNumeric,

    [PgName("SCORES_CATEGORICAL")]
    ScoresCategorical,

    [PgName("SCORES_BOOLEAN")]
    ScoresBoolean,
}
