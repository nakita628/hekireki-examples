use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "ApiKeyPermission")]
pub enum ApiKeyPermission {
    #[sea_orm(string_value = "read")]
    Read,
    #[sea_orm(string_value = "write")]
    Write,
    #[sea_orm(string_value = "manage")]
    Manage,
}
