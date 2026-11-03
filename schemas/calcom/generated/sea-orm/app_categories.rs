use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "AppCategories")]
pub enum AppCategories {
    #[sea_orm(string_value = "calendar")]
    Calendar,
    #[sea_orm(string_value = "messaging")]
    Messaging,
    #[sea_orm(string_value = "other")]
    Other,
    #[sea_orm(string_value = "payment")]
    Payment,
    #[sea_orm(string_value = "video")]
    Video,
    #[sea_orm(string_value = "web3")]
    Web3,
    #[sea_orm(string_value = "automation")]
    Automation,
    #[sea_orm(string_value = "analytics")]
    Analytics,
    #[sea_orm(string_value = "conferencing")]
    Conferencing,
    #[sea_orm(string_value = "crm")]
    Crm,
}
