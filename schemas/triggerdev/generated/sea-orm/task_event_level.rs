use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "TaskEventLevel")]
pub enum TaskEventLevel {
    #[sea_orm(string_value = "TRACE")]
    Trace,
    #[sea_orm(string_value = "DEBUG")]
    Debug,
    #[sea_orm(string_value = "LOG")]
    Log,
    #[sea_orm(string_value = "INFO")]
    Info,
    #[sea_orm(string_value = "WARN")]
    Warn,
    #[sea_orm(string_value = "ERROR")]
    Error,
}
