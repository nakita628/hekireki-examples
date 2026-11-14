use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "CostType")]
pub enum CostType {
    #[sea_orm(string_value = "TRACE_CHECK")]
    TraceCheck,
    #[sea_orm(string_value = "GUARDRAIL")]
    Guardrail,
    #[sea_orm(string_value = "CLUSTERING")]
    Clustering,
    #[sea_orm(string_value = "BATCH_EVALUATION")]
    BatchEvaluation,
}
