use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(rs_type = "String", db_type = "Enum", enum_name = "MonitorSeverity")]
pub enum MonitorSeverity {
    #[sea_orm(string_value = "PAUSED")]
    Paused,
    #[sea_orm(string_value = "UNKNOWN")]
    Unknown,
    #[sea_orm(string_value = "NO_DATA")]
    NoData,
    #[sea_orm(string_value = "OK")]
    Ok,
    #[sea_orm(string_value = "WARNING")]
    Warning,
    #[sea_orm(string_value = "ALERT")]
    Alert,
}
