use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "WorkflowStatus")]
pub enum WorkflowStatus {
    #[sea_orm(string_value = "draft")]
    Draft,
    #[sea_orm(string_value = "enabled")]
    Enabled,
    #[sea_orm(string_value = "disabled")]
    Disabled,
    #[sea_orm(string_value = "archived")]
    Archived,
}
