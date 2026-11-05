use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ThreadTrackerType")]
pub enum ThreadTrackerType {
    #[sea_orm(string_value = "AWAITING")]
    Awaiting,
    #[sea_orm(string_value = "NEEDS_REPLY")]
    NeedsReply,
    #[sea_orm(string_value = "NEEDS_ACTION")]
    NeedsAction,
}
