use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "AiTaggingMethod")]
pub enum AiTaggingMethod {
    #[sea_orm(string_value = "DISABLED")]
    Disabled,
    #[sea_orm(string_value = "GENERATE")]
    Generate,
    #[sea_orm(string_value = "EXISTING")]
    Existing,
    #[sea_orm(string_value = "PREDEFINED")]
    Predefined,
}
