#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum EvaluationExecutionMode
{
    [PgName("ON_MESSAGE")]
    OnMessage,

    [PgName("AS_GUARDRAIL")]
    AsGuardrail,

    [PgName("MANUALLY")]
    Manually,
}
