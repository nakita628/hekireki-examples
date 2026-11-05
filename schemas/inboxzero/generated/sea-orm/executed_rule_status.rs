use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ExecutedRuleStatus")]
pub enum ExecutedRuleStatus {
    #[sea_orm(string_value = "APPLIED")]
    Applied,
    #[sea_orm(string_value = "APPLYING")]
    Applying,
    #[sea_orm(string_value = "REJECTED")]
    Rejected,
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "SKIPPED")]
    Skipped,
    #[sea_orm(string_value = "ERROR")]
    Error,
}
