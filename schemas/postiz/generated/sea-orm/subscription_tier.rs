use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SubscriptionTier")]
pub enum SubscriptionTier {
    #[sea_orm(string_value = "STANDARD")]
    Standard,
    #[sea_orm(string_value = "PRO")]
    Pro,
    #[sea_orm(string_value = "TEAM")]
    Team,
    #[sea_orm(string_value = "ULTIMATE")]
    Ultimate,
}
