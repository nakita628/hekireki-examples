use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "GatewayIngestionMode"
)]
pub enum GatewayIngestionMode {
    #[sea_orm(string_value = "none")]
    None,
    #[sea_orm(string_value = "usage")]
    Usage,
    #[sea_orm(string_value = "full")]
    Full,
}
