use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "EvalTemplateType")]
pub enum EvalTemplateType {
    #[sea_orm(string_value = "LLM_AS_JUDGE")]
    LlmAsJudge,
    #[sea_orm(string_value = "CODE")]
    Code,
    #[sea_orm(string_value = "DECISION_MODEL")]
    DecisionModel,
    #[sea_orm(string_value = "FACET")]
    Facet,
}
