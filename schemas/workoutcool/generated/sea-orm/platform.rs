use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "Platform")]
pub enum Platform {
    #[sea_orm(string_value = "WEB")]
    Web,
    #[sea_orm(string_value = "IOS")]
    Ios,
    #[sea_orm(string_value = "ANDROID")]
    Android,
}
