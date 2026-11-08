use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "CampaignAudienceType"
)]
pub enum CampaignAudienceType {
    #[sea_orm(string_value = "ALL")]
    All,
    #[sea_orm(string_value = "FILTERED")]
    Filtered,
    #[sea_orm(string_value = "SEGMENT")]
    Segment,
}
