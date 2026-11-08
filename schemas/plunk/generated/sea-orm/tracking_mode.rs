use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "TrackingMode")]
pub enum TrackingMode {
    #[sea_orm(string_value = "ENABLED")]
    Enabled,
    #[sea_orm(string_value = "DISABLED")]
    Disabled,
    #[sea_orm(string_value = "MARKETING_ONLY")]
    MarketingOnly,
}
