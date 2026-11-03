use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "Provider")]
pub enum Provider {
    #[sea_orm(string_value = "ANONYMOUS")]
    Anonymous,
    #[sea_orm(string_value = "GOOGLE")]
    Google,
    #[sea_orm(string_value = "INTERNET_IDENTITY")]
    InternetIdentity,
    #[sea_orm(string_value = "OIDC")]
    Oidc,
}
