use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "space_tiers")]
pub enum SpaceTier {
    #[sea_orm(string_value = "hobby")]
    Hobby,
    #[sea_orm(string_value = "pro")]
    Pro,
}
