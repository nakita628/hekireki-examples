use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "AlertType")]
pub enum AlertType {
    #[sea_orm(string_value = "CRITICAL")]
    Critical,
    #[sea_orm(string_value = "WARNING")]
    Warning,
    #[sea_orm(string_value = "INFO")]
    Info,
}
