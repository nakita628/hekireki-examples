use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "PricingModel")]
pub enum PricingModel {
    #[sea_orm(string_value = "TIERED")]
    Tiered,
    #[sea_orm(string_value = "SEAT_EVENT")]
    SeatEvent,
}
