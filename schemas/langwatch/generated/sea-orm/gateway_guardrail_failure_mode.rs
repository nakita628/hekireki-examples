use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "GatewayGuardrailFailureMode"
)]
pub enum GatewayGuardrailFailureMode {
    #[sea_orm(string_value = "FAIL_OPEN")]
    FailOpen,
    #[sea_orm(string_value = "FAIL_CLOSED")]
    FailClosed,
}
