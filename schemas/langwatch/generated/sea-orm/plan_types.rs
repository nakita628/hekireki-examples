use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "PlanTypes")]
pub enum PlanTypes {
    #[sea_orm(string_value = "FREE")]
    Free,
    #[sea_orm(string_value = "PRO")]
    Pro,
    #[sea_orm(string_value = "GROWTH")]
    Growth,
    #[sea_orm(string_value = "GROWTH_SEAT_EUR_MONTHLY")]
    GrowthSeatEurMonthly,
    #[sea_orm(string_value = "GROWTH_SEAT_EUR_ANNUAL")]
    GrowthSeatEurAnnual,
    #[sea_orm(string_value = "GROWTH_SEAT_USD_MONTHLY")]
    GrowthSeatUsdMonthly,
    #[sea_orm(string_value = "GROWTH_SEAT_USD_ANNUAL")]
    GrowthSeatUsdAnnual,
    #[sea_orm(string_value = "ENTERPRISE")]
    Enterprise,
    #[sea_orm(string_value = "LAUNCH")]
    Launch,
    #[sea_orm(string_value = "ACCELERATE")]
    Accelerate,
    #[sea_orm(string_value = "LAUNCH_ANNUAL")]
    LaunchAnnual,
    #[sea_orm(string_value = "ACCELERATE_ANNUAL")]
    AccelerateAnnual,
}
