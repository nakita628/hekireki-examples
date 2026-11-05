use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "IntegrationType")]
pub enum IntegrationType {
    #[sea_orm(string_value = "app")]
    App,
    #[sea_orm(string_value = "mail")]
    Mail,
    #[sea_orm(string_value = "custom")]
    Custom,
}
