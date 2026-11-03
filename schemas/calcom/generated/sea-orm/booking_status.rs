use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "BookingStatus")]
pub enum BookingStatus {
    #[sea_orm(string_value = "cancelled")]
    Cancelled,
    #[sea_orm(string_value = "accepted")]
    Accepted,
    #[sea_orm(string_value = "rejected")]
    Rejected,
    #[sea_orm(string_value = "pending")]
    Pending,
    #[sea_orm(string_value = "awaiting_host")]
    AwaitingHost,
}
