use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ChallengeStatus")]
pub enum ChallengeStatus {
    #[sea_orm(string_value = "DECLINED")]
    Declined,
    #[sea_orm(string_value = "BANNED")]
    Banned,
    #[sea_orm(string_value = "PENDING")]
    Pending,
    #[sea_orm(string_value = "ACTIVE")]
    Active,
}
