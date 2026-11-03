use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SMSLockState")]
pub enum SMSLockState {
    #[sea_orm(string_value = "LOCKED")]
    Locked,
    #[sea_orm(string_value = "UNLOCKED")]
    Unlocked,
    #[sea_orm(string_value = "REVIEW_NEEDED")]
    ReviewNeeded,
}
