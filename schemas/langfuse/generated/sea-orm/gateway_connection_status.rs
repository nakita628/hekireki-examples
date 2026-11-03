use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "GatewayConnectionStatus"
)]
pub enum GatewayConnectionStatus {
    #[sea_orm(string_value = "enabled")]
    Enabled,
    #[sea_orm(string_value = "disabled")]
    Disabled,
    #[sea_orm(string_value = "error")]
    Error,
}
