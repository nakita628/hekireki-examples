use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "LinkAudienceType")]
pub enum LinkAudienceType {
    #[sea_orm(string_value = "GENERAL")]
    General,
    #[sea_orm(string_value = "GROUP")]
    Group,
    #[sea_orm(string_value = "TEAM")]
    Team,
}
