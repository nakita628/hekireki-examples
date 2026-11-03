use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "RRResetInterval")]
pub enum RRResetInterval {
    #[sea_orm(string_value = "MONTH")]
    Month,
    #[sea_orm(string_value = "DAY")]
    Day,
}
