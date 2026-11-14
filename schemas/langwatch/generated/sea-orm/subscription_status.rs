use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SubscriptionStatus")]
pub enum SubscriptionStatus {
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "FAILED")]
    Failed,
    #[sea_orm(string_value = "ACTIVE")]
    Active,
    #[sea_orm(string_value = "CANCELLED")]
    Cancelled,
}
