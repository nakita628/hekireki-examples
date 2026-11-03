use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "DashboardSectionType"
)]
pub enum DashboardSectionType {
    #[sea_orm(string_value = "STATS")]
    Stats,
    #[sea_orm(string_value = "RECENT_LINKS")]
    RecentLinks,
    #[sea_orm(string_value = "PINNED_LINKS")]
    PinnedLinks,
    #[sea_orm(string_value = "COLLECTION")]
    Collection,
}
