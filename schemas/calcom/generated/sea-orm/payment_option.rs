use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "PaymentOption")]
pub enum PaymentOption {
    #[sea_orm(string_value = "ON_BOOKING")]
    OnBooking,
    #[sea_orm(string_value = "HOLD")]
    Hold,
}
