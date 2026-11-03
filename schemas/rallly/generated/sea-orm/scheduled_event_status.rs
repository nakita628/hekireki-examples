use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "scheduled_event_status"
)]
pub enum ScheduledEventStatus {
    #[sea_orm(string_value = "confirmed")]
    Confirmed,
    #[sea_orm(string_value = "canceled")]
    Canceled,
    #[sea_orm(string_value = "unconfirmed")]
    Unconfirmed,
}
