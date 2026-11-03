use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "poll_status")]
pub enum PollStatus {
    #[sea_orm(string_value = "open")]
    Open,
    #[sea_orm(string_value = "closed")]
    Closed,
    #[sea_orm(string_value = "scheduled")]
    Scheduled,
    #[sea_orm(string_value = "canceled")]
    Canceled,
}
