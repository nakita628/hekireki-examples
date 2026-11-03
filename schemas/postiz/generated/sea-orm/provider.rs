use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "Provider")]
pub enum Provider {
    #[sea_orm(string_value = "LOCAL")]
    Local,
    #[sea_orm(string_value = "GITHUB")]
    Github,
    #[sea_orm(string_value = "GOOGLE")]
    Google,
    #[sea_orm(string_value = "APPLE")]
    Apple,
    #[sea_orm(string_value = "FARCASTER")]
    Farcaster,
    #[sea_orm(string_value = "WALLET")]
    Wallet,
    #[sea_orm(string_value = "GENERIC")]
    Generic,
}
