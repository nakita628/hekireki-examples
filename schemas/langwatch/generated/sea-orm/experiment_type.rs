use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ExperimentType")]
pub enum ExperimentType {
    #[sea_orm(string_value = "DSPY")]
    Dspy,
    #[sea_orm(string_value = "BATCH_EVALUATION")]
    BatchEvaluation,
    #[sea_orm(string_value = "BATCH_EVALUATION_V2")]
    BatchEvaluationV2,
    #[sea_orm(string_value = "EVALUATIONS_V3")]
    EvaluationsV3,
}
