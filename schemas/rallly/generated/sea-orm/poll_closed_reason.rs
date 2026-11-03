use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "poll_closed_reason")]
pub enum PollClosedReason {
    #[sea_orm(string_value = "auto")]
    Auto,
    #[sea_orm(string_value = "manual")]
    Manual,
}
