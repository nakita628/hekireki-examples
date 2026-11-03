#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum EvalTemplateType
{
    [PgName("LLM_AS_JUDGE")]
    LlmAsJudge,

    [PgName("CODE")]
    Code,

    [PgName("DECISION_MODEL")]
    DecisionModel,

    [PgName("FACET")]
    Facet,
}
