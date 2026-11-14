#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum ExperimentType
{
    [PgName("DSPY")]
    Dspy,

    [PgName("BATCH_EVALUATION")]
    BatchEvaluation,

    [PgName("BATCH_EVALUATION_V2")]
    BatchEvaluationV2,

    [PgName("EVALUATIONS_V3")]
    EvaluationsV3,
}
