use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "LinkScope")]
pub enum LinkScope {
    #[sea_orm(string_value = "workspace")]
    Workspace,
    #[sea_orm(string_value = "folders")]
    Folders,
    #[sea_orm(string_value = "links")]
    Links,
}
