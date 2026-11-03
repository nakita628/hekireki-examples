use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "IdentityProvider")]
pub enum IdentityProvider {
    #[sea_orm(string_value = "DOCUMENSO")]
    Documenso,
    #[sea_orm(string_value = "GOOGLE")]
    Google,
    #[sea_orm(string_value = "OIDC")]
    Oidc,
}
