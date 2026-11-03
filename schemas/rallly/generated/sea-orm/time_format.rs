use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "time_format")]
pub enum TimeFormat {
    #[sea_orm(string_value = "hours12")]
    Hours12,
    #[sea_orm(string_value = "hours24")]
    Hours24,
}
