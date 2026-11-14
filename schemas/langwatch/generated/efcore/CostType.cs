#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum CostType
{
    [PgName("TRACE_CHECK")]
    TraceCheck,

    [PgName("GUARDRAIL")]
    Guardrail,

    [PgName("CLUSTERING")]
    Clustering,

    [PgName("BATCH_EVALUATION")]
    BatchEvaluation,
}
