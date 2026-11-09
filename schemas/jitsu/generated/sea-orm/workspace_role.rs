use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "WorkspaceRole")]
pub enum WorkspaceRole {
    #[sea_orm(string_value = "owner")]
    Owner,
    #[sea_orm(string_value = "editor")]
    Editor,
    #[sea_orm(string_value = "analyst")]
    Analyst,
}
