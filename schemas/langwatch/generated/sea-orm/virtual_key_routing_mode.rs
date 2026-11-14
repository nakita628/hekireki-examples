use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "VirtualKeyRoutingMode"
)]
pub enum VirtualKeyRoutingMode {
    #[sea_orm(string_value = "NONE")]
    None,
    #[sea_orm(string_value = "FALLBACK_ALL")]
    FallbackAll,
    #[sea_orm(string_value = "POLICY")]
    Policy,
}
