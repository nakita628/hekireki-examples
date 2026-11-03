use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "EvaluatorBlockReason"
)]
pub enum EvaluatorBlockReason {
    #[sea_orm(string_value = "LLM_CONNECTION_AUTH_INVALID")]
    LlmConnectionAuthInvalid,
    #[sea_orm(string_value = "LLM_CONNECTION_BILLING_EXHAUSTED")]
    LlmConnectionBillingExhausted,
    #[sea_orm(string_value = "LLM_CONNECTION_ENDPOINT_UNREACHABLE")]
    LlmConnectionEndpointUnreachable,
    #[sea_orm(string_value = "LLM_CONNECTION_MISSING")]
    LlmConnectionMissing,
    #[sea_orm(string_value = "DEFAULT_EVAL_MODEL_MISSING")]
    DefaultEvalModelMissing,
    #[sea_orm(string_value = "EVAL_MODEL_CONFIG_INVALID")]
    EvalModelConfigInvalid,
    #[sea_orm(string_value = "EVAL_MODEL_UNAVAILABLE")]
    EvalModelUnavailable,
    #[sea_orm(string_value = "PROVIDER_ACCOUNT_NOT_READY")]
    ProviderAccountNotReady,
}
