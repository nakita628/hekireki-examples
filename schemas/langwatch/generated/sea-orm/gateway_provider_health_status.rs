use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "GatewayProviderHealthStatus"
)]
pub enum GatewayProviderHealthStatus {
    #[sea_orm(string_value = "UNKNOWN")]
    Unknown,
    #[sea_orm(string_value = "HEALTHY")]
    Healthy,
    #[sea_orm(string_value = "DEGRADED")]
    Degraded,
    #[sea_orm(string_value = "CIRCUIT_OPEN")]
    CircuitOpen,
}
