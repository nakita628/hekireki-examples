use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "GatewayBudgetWindow"
)]
pub enum GatewayBudgetWindow {
    #[sea_orm(string_value = "MINUTE")]
    Minute,
    #[sea_orm(string_value = "HOUR")]
    Hour,
    #[sea_orm(string_value = "DAY")]
    Day,
    #[sea_orm(string_value = "WEEK")]
    Week,
    #[sea_orm(string_value = "MONTH")]
    Month,
    #[sea_orm(string_value = "TOTAL")]
    Total,
    #[sea_orm(string_value = "MANUAL")]
    Manual,
}
