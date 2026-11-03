use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ReminderType")]
pub enum ReminderType {
    #[sea_orm(string_value = "PENDING_BOOKING_CONFIRMATION")]
    PendingBookingConfirmation,
}
