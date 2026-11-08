use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "StepExecutionStatus"
)]
pub enum StepExecutionStatus {
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "SCHEDULED")]
    Scheduled,
    #[sea_orm(string_value = "WAITING")]
    Waiting,
    #[sea_orm(string_value = "RUNNING")]
    Running,
    #[sea_orm(string_value = "COMPLETED")]
    Completed,
    #[sea_orm(string_value = "SKIPPED")]
    Skipped,
    #[sea_orm(string_value = "FAILED")]
    Failed,
}
