use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "IdentityProvider")]
pub enum IdentityProvider {
    #[sea_orm(string_value = "email")]
    Email,
    #[sea_orm(string_value = "github")]
    Github,
    #[sea_orm(string_value = "google")]
    Google,
    #[sea_orm(string_value = "azuread")]
    Azuread,
    #[sea_orm(string_value = "openid")]
    Openid,
    #[sea_orm(string_value = "saml")]
    Saml,
}
