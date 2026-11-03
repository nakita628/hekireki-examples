#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum EvaluatorBlockReason
{
    [PgName("LLM_CONNECTION_AUTH_INVALID")]
    LlmConnectionAuthInvalid,

    [PgName("LLM_CONNECTION_BILLING_EXHAUSTED")]
    LlmConnectionBillingExhausted,

    [PgName("LLM_CONNECTION_ENDPOINT_UNREACHABLE")]
    LlmConnectionEndpointUnreachable,

    [PgName("LLM_CONNECTION_MISSING")]
    LlmConnectionMissing,

    [PgName("DEFAULT_EVAL_MODEL_MISSING")]
    DefaultEvalModelMissing,

    [PgName("EVAL_MODEL_CONFIG_INVALID")]
    EvalModelConfigInvalid,

    [PgName("EVAL_MODEL_UNAVAILABLE")]
    EvalModelUnavailable,

    [PgName("PROVIDER_ACCOUNT_NOT_READY")]
    ProviderAccountNotReady,
}
