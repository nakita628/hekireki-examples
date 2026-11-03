use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "CalendarCacheEventStatus"
)]
pub enum CalendarCacheEventStatus {
    #[sea_orm(string_value = "confirmed")]
    Confirmed,
    #[sea_orm(string_value = "tentative")]
    Tentative,
    #[sea_orm(string_value = "cancelled")]
    Cancelled,
}
