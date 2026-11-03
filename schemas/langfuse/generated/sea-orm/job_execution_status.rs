use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "JobExecutionStatus")]
pub enum JobExecutionStatus {
    #[sea_orm(string_value = "COMPLETED")]
    Completed,
    #[sea_orm(string_value = "ERROR")]
    Error,
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "CANCELLED")]
    Cancelled,
    #[sea_orm(string_value = "DELAYED")]
    Delayed,
}
