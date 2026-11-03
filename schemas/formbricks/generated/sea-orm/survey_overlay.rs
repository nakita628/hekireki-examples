use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "SurveyOverlay")]
pub enum SurveyOverlay {
    #[sea_orm(string_value = "none")]
    None,
    #[sea_orm(string_value = "light")]
    Light,
    #[sea_orm(string_value = "dark")]
    Dark,
}
