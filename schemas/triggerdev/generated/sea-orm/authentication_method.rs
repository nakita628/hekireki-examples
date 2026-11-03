use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "AuthenticationMethod"
)]
pub enum AuthenticationMethod {
    #[sea_orm(string_value = "GITHUB")]
    Github,
    #[sea_orm(string_value = "MAGIC_LINK")]
    MagicLink,
    #[sea_orm(string_value = "GOOGLE")]
    Google,
    #[sea_orm(string_value = "SSO")]
    Sso,
}
