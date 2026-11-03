use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "PeriodType")]
pub enum PeriodType {
    #[sea_orm(string_value = "unlimited")]
    Unlimited,
    #[sea_orm(string_value = "rolling")]
    Rolling,
    #[sea_orm(string_value = "rolling_window")]
    RollingWindow,
    #[sea_orm(string_value = "range")]
    Range,
}
