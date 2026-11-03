use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "DataDbConnectionHealthState"
)]
pub enum DataDbConnectionHealthState {
    #[sea_orm(string_value = "healthy")]
    Healthy,
    #[sea_orm(string_value = "read_only")]
    ReadOnly,
    #[sea_orm(string_value = "unreachable")]
    Unreachable,
    #[sea_orm(string_value = "degraded")]
    Degraded,
}
