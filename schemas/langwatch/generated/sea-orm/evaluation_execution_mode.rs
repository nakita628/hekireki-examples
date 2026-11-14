use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "EvaluationExecutionMode"
)]
pub enum EvaluationExecutionMode {
    #[sea_orm(string_value = "ON_MESSAGE")]
    OnMessage,
    #[sea_orm(string_value = "AS_GUARDRAIL")]
    AsGuardrail,
    #[sea_orm(string_value = "MANUALLY")]
    Manually,
}
