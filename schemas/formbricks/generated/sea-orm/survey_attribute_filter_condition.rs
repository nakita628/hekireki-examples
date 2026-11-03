use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "SurveyAttributeFilterCondition"
)]
pub enum SurveyAttributeFilterCondition {
    #[sea_orm(string_value = "equals")]
    Equals,
    #[sea_orm(string_value = "notEquals")]
    Notequals,
}
