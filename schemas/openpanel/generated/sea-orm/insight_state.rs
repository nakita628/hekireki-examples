use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "InsightState")]
pub enum InsightState {
    #[sea_orm(string_value = "active")]
    Active,
    #[sea_orm(string_value = "suppressed")]
    Suppressed,
    #[sea_orm(string_value = "closed")]
    Closed,
}
