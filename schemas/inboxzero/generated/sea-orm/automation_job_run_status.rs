use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "AutomationJobRunStatus"
)]
pub enum AutomationJobRunStatus {
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "RUNNING")]
    Running,
    #[sea_orm(string_value = "SENT")]
    Sent,
    #[sea_orm(string_value = "SKIPPED")]
    Skipped,
    #[sea_orm(string_value = "FAILED")]
    Failed,
}
