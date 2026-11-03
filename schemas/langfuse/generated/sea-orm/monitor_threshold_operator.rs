use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "MonitorThresholdOperator"
)]
pub enum MonitorThresholdOperator {
    #[sea_orm(string_value = "GT")]
    Gt,
    #[sea_orm(string_value = "GTE")]
    Gte,
    #[sea_orm(string_value = "LT")]
    Lt,
    #[sea_orm(string_value = "LTE")]
    Lte,
    #[sea_orm(string_value = "EQ")]
    Eq,
    #[sea_orm(string_value = "NEQ")]
    Neq,
}
