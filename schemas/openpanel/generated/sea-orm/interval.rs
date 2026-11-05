use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "Interval")]
pub enum Interval {
    #[sea_orm(string_value = "hour")]
    Hour,
    #[sea_orm(string_value = "day")]
    Day,
    #[sea_orm(string_value = "month")]
    Month,
    #[sea_orm(string_value = "minute")]
    Minute,
    #[sea_orm(string_value = "week")]
    Week,
}
