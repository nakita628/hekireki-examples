use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "CreativeType")]
pub enum CreativeType {
    #[sea_orm(string_value = "feedbackButton")]
    Feedbackbutton,
    #[sea_orm(string_value = "banner")]
    Banner,
}
